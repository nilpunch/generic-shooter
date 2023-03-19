using UnityEngine;

namespace SM.FPS
{
	public class CharacterPlatformVelocityInheritance : CharacterPhysicsModule
	{
		[SerializeField] private FootRaycast _footRaycast;

		[SerializeField, Range(0f, 90f)] private float _maxInheritanceAngle = 90f;
		[SerializeField] private float _minInheritanceDistance = 0.1f;
		
		public override void Affect(IPhysics physics)
		{
			foreach (var collider in _footRaycast.FootHit.CollidersWithAngleAndDistance(_maxInheritanceAngle, _minInheritanceDistance))
			{
				if (collider.TryGetComponent<IMovingPlatform>(out var movingPlatform))
				{
					movingPlatform.ForwardVelocityTo(physics);
				}
			}
		}
	}
}