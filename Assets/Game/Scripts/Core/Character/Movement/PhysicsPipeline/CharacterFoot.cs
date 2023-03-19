using UnityEngine;

namespace SM.FPS
{
	public class CharacterFoot : MonoBehaviour
	{
		[SerializeField] private FootRaycast _footRaycast;
		[SerializeField, Range(0f, 90f)] private float _maxGroundJumpAngle = 40f;
		[SerializeField, Min(0f)] private float _maxGroundDistance = 0.01f;
		[SerializeField] private float _cayoteTime = 0.1f;

		private float _elapsedAirTime;
		
		public bool CanJumpOff { get; private set; }

		public void UpdateGroundState()
		{
			bool canJump = false;

			if (_footRaycast.FootHit.HasHit)
			{
				float angleToGround = Vector3.Angle(Vector3.up, _footRaycast.FootHit.AverageNormal());
				
				bool hitGroundAtExceptedAngle = angleToGround < _maxGroundJumpAngle;
				bool hitGroundAtExceptedDistance = _footRaycast.FootHit.MinDistanceWithAngleConstraint(_maxGroundJumpAngle) < _maxGroundDistance;
				canJump = hitGroundAtExceptedAngle && hitGroundAtExceptedDistance;
			}
			
			if (canJump)
			{
				CanJumpOff = true;
				_elapsedAirTime = 0f;
			}
			else if (_elapsedAirTime < _cayoteTime)
			{
				_elapsedAirTime += Time.deltaTime;
				CanJumpOff = true;
			}
			else
			{
				CanJumpOff = false;
			}
		}
	}
}