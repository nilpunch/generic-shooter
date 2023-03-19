using UnityEngine;

namespace SM.FPS
{
	public class CharacterVerticalMovement : CharacterPhysicsModule
	{
		[SerializeField] private CharacterFoot _characterFoot;
		[SerializeField] private CharacterGravity _characterGravity;
		[SerializeField] private float _jumpMinHeight = 2f;
		[SerializeField] private float _jumpMaxHeight = 4.5f;
		[SerializeField] private float _jumpGravityDevider = 1.3f;
		[SerializeField] private float _earlyFallGravityMultiplier = 3.0f;
		[SerializeField, Range(0, 2)] private int _airJumps = 0;
		[SerializeField] private float _jumpBuffer = 0.2f;
		
		private bool _inJump;
		private bool _fallRequested;
		private bool _falling;
		private int _availableAirJumps;

		private bool _jumpQueued;
		private float _jumpBufferElapsedTime;

		private float JumpGravity => _characterGravity.GravityMagnitude / _jumpGravityDevider;
		
		private float JumpTime => Mathf.Sqrt(2f * _jumpMaxHeight / JumpGravity);

		private float EarlyFallGravity => JumpGravity * _earlyFallGravityMultiplier;

		private float JumpStartVelocity => 2f * _jumpMaxHeight / JumpTime;

		private float FallMinVelocity => JumpStartVelocity - JumpGravity * MinJumpTime;
		
		private float MinJumpTime => (-JumpStartVelocity + Mathf.Sqrt(JumpStartVelocity * JumpStartVelocity - 2f * JumpGravity * _jumpMinHeight)) / -JumpGravity;
		
		public override void Affect(IPhysics physics)
		{
			bool isFallVelocityReached = physics.Velocity.y <= FallMinVelocity;
			bool isFalling = physics.Velocity.y <= 0.001f;
			
			if (isFalling)
			{
				_falling = true;
			}

			if (_falling && _characterFoot.CanJumpOff)
			{
				_availableAirJumps = _airJumps;
				_inJump = false;
			}

			Vector3 additionalGravity = Vector3.zero;
			if (_fallRequested && isFallVelocityReached && !_falling)
				additionalGravity = _characterGravity.WorldUp * (_characterGravity.GravityMagnitude - EarlyFallGravity);
			else if (!_falling)
				additionalGravity = _characterGravity.WorldUp * (_characterGravity.GravityMagnitude - JumpGravity);

			_jumpBufferElapsedTime += Time.deltaTime;

			bool isBufferExpired = _jumpBufferElapsedTime > _jumpBuffer;
			bool isInAir = _inJump || !_characterFoot.CanJumpOff;
			bool dontHaveAirJumps = _availableAirJumps == 0;
				
			if (!_jumpQueued || isBufferExpired || isInAir && dontHaveAirJumps)
			{
				physics.AddForce(additionalGravity * Time.deltaTime);
				return;
			}
			
			_jumpQueued = false;
				
			if (_inJump)
				_availableAirJumps -= 1;
			
			_inJump = true;
			_fallRequested = false;
			_falling = false;

			physics.AddForce(new Vector3(0f, JumpStartVelocity - physics.Velocity.y, 0f));
		}

		public void Jump()
		{
			_jumpQueued = true;
			_jumpBufferElapsedTime = 0f;
		}

		public void Fall()
		{
			_jumpQueued = false;
			_fallRequested = true;
		}
	}
}