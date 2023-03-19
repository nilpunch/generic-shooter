using UnityEngine;

namespace SM.FPS
{
	public class RotatingPlatform : Platform, IMovingPlatform
	{
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private Vector3 _rotationDirection = Vector3.up;
		[SerializeField] private float _rotationSpeed = 1f;

		private Vector3 _rotationAxis;
		private Quaternion _startRotation;
		
		private void Awake()
		{
			_rotationAxis = _rigidbody.rotation * _rotationDirection;
		}

		public override void ManualUpdate()
		{
			float rotationSpeedDegrees = _rotationSpeed * Mathf.Rad2Deg;
			
			_rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.AngleAxis(rotationSpeedDegrees * Time.deltaTime, _rotationDirection));
		}

		public void ForwardVelocityTo(IPhysics physics)
		{
			float angularVelocity = Vector3.ProjectOnPlane(_rigidbody.position - physics.Position, _rotationAxis).magnitude 
			                        * _rotationSpeed;
			Vector3 velocityTangent = Vector3.Cross(Vector3.Normalize(_rigidbody.position - physics.Position), _rotationAxis);
			physics.AddInheritedForce(velocityTangent * angularVelocity);
		}
	}
}