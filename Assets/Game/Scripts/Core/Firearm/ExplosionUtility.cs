using UnityEngine;

namespace SM.FPS
{
	public static class ExplosionUtility
	{
		private static readonly RaycastHit[] s_raycastResults = new RaycastHit[256];

		public static void RadiusBlast(Vector3 position, float radius, LayerMask layers, float damage)
		{
			int hitsAmount = Physics.SphereCastNonAlloc(position, radius, Vector3.up, s_raycastResults, 0f, layers);

			for (int i = 0; i < hitsAmount; i++)
			{
				var hit = s_raycastResults[i];

				if (hit.collider.TryGetComponent<DamageTarget>(out var damageTarget))
				{
					float damageByDistance = hit.distance / radius * damage;
					damageTarget.TakeDamage(damageByDistance);
				}
			}
		}

		public static void RaycastBlast(Vector3 position, float radius, LayerMask layers, float damage, int raysAmount = 64)
		{
			float damagePerRay = raysAmount / damage * 2f;
			
			for (int index = 0; index < raysAmount; index++)
			{
				var direction = PointDistributedOnSphere(index, raysAmount);
				
				if (Physics.Raycast(position, direction, out var hitInfo, radius, layers))
				{
					if (hitInfo.collider.TryGetComponent<DamageTarget>(out var damageTarget))
					{
						float damageByDistance = hitInfo.distance / radius * damagePerRay;
						damageTarget.TakeDamage(damageByDistance);
					}
				}
			}
		}

		private static float s_goldenAngle = Mathf.PI * (3f - Mathf.Sqrt(5f));
		
		private static Vector3 PointDistributedOnSphere(int index, int total)
		{
			// y goes from -1 to 1
			var y = index / (total - 1f) * 2f - 1f;

			// golden angle increment
			var theta = s_goldenAngle * index;

			var radiusAtY = Mathf.Sqrt(1 - y * y);

			var x = Mathf.Cos(theta) * radiusAtY;
			var z = Mathf.Sin(theta) * radiusAtY;

			return new Vector3(x, y, z);
		}
	}
}