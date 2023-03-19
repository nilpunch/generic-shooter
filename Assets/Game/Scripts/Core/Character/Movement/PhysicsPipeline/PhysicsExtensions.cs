using UnityEngine;

namespace SM.FPS
{
	public static class PhysicsExtensions
	{
		public static float ContributingInDepenetration(this IPhysics physics, Vector3 force)
		{
			return Mathf.Sqrt(Mathf.Clamp(Vector3.Dot(force, physics.Depenetration) / 2f, 0f, float.MaxValue));
		}
	}
}