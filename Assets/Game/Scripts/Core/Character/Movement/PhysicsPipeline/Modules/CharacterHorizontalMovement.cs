using UnityEngine;

namespace SM.FPS
{
	public class CharacterHorizontalMovement : CharacterPhysicsModule
	{
		[SerializeField] private FootRaycast _footRaycast;
		[SerializeField] private CharacterGravity _characterGravity;
		
		[Header("Movement")]
		[SerializeField] private float _maxSpeed = 14f;
		[SerializeField] private float _acceleration = 80f;
		[SerializeField] private float _deceleration = 200f;

		[Header("Slope movement")] 
		[SerializeField, Range(0f, 90f)] private float _tolerantSlope = 45f;
		[SerializeField, Range(0f, 90f)] private float _maxSlope = 60f;
		
		[Header("Gravity fight")]
		[SerializeField, Range(0f, 90f)] private float _noSlipAngle = 45f;
		[SerializeField, Range(0f, 90f)] private float _fullSlipAngle = 60f;
		[SerializeField] private float _slipMultiplier = 2f;
		
		[Header("Other")]
		[SerializeField] private float _maxGroundDistance = 0.01f;

		private Vector3 _moveDirection;
		
		public override void Affect(IPhysics physics)
		{
			// Defying normal to ground
			Vector3 groundNormal = _characterGravity.WorldUp;
			if (_footRaycast.FootHit.HasHit)
			{
				float distanceToGround = _footRaycast.FootHit.MinDistance();

				if (distanceToGround < _maxGroundDistance)
					groundNormal = _footRaycast.FootHit.NearestNormal();
			}
			
			Vector3 desiredVelocity = _maxSpeed * _moveDirection.magnitude * Vector3.ProjectOnPlane(_moveDirection, groundNormal).normalized;

			Vector3 lastVelocity = Vector3.ProjectOnPlane(physics.Velocity, groundNormal);

			// Choosing acceleration
			float decelerationFactor = Mathf.Clamp01(-Vector3.Dot(lastVelocity, desiredVelocity - lastVelocity) / (_maxSpeed * _maxSpeed));
			float acceleration = Mathf.Lerp(_acceleration, _deceleration, decelerationFactor);

			Vector3 velocity = Vector3.MoveTowards(lastVelocity, desiredVelocity, acceleration * Time.deltaTime);

			// Movement against slope
			Vector3 slopeResistance = Vector3.zero;
			if (Vector3.Angle(_characterGravity.WorldUp, groundNormal) > 0.001f)
			{
				Vector3 slope = Vector3.ProjectOnPlane(_characterGravity.WorldUp, groundNormal).normalized;
				Vector3 fullSlopeResistance = Vector3.Project(velocity, slope);

				float resistanceToWorldUpAngle = fullSlopeResistance.sqrMagnitude > 0.001f
					? Vector3.Angle(fullSlopeResistance, _characterGravity.WorldUp)
					: 90f;
				float fightAgainstSlope = Mathf.Clamp01(Mathf.InverseLerp(_tolerantSlope, _maxSlope, 90f - resistanceToWorldUpAngle));

				slopeResistance = fullSlopeResistance * fightAgainstSlope;
			}
			
			// Fight against gravity, simulating slip
			float groundUpToWorldUpAngle = Vector3.Angle(groundNormal, _characterGravity.WorldUp);
			float gravityFighting = 1f - Mathf.Clamp01(Mathf.InverseLerp(_noSlipAngle, _fullSlipAngle, groundUpToWorldUpAngle));
			Vector3 fightAgainstGravity = Vector3.ProjectOnPlane(-_characterGravity.Affection, groundNormal) 
			                              * Mathf.Lerp(-_slipMultiplier, 1f, gravityFighting);
			
			Vector3 velocityCorrection = -lastVelocity + velocity - slopeResistance + fightAgainstGravity;
			
			physics.AddForce(velocityCorrection);
		}

		public void Move(Vector3 movement)
		{
			_moveDirection = Vector3.Scale(Vector3.ClampMagnitude(movement, 1f), new Vector3(1f, 0f, 1f));
		}
	}
}