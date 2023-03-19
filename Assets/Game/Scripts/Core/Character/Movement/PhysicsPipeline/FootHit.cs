using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SM.FPS
{
	public readonly struct FootHit
	{
		public readonly IReadOnlyList<RaycastHit> Hits;
		public readonly float RaysIndent;
		public readonly float RaycastDistance;

		public FootHit(IReadOnlyList<RaycastHit> readOnlyList, float raysIndent, float raycastDistance)
		{
			RaycastDistance = raycastDistance;
			Hits = readOnlyList;
			RaysIndent = raysIndent;
		}

		public bool HasHit => Hits.Any(raycastHit => raycastHit.collider is not null);
		
		public IEnumerable<Collider> Colliders => Hits
			.Where(hit => hit.collider is not null)
			.Select(hit => hit.collider)
			.Distinct();

		public IEnumerable<Collider> CollidersWithAngleAndDistance(float maxAngle, float minDistance)
		{
			return AllCollidersWithAngle(maxAngle, minDistance).Distinct();
		}

		private IEnumerable<Collider> AllCollidersWithAngle(float maxAngle, float minDistance)
		{
			foreach (var raycastHit in Hits.Where(raycastHit => raycastHit.collider is not null))
			{
				float distance = raycastHit.distance - RaysIndent;
				float angle = Vector3.Angle(Vector3.up, raycastHit.normal);
				
				if (distance <= minDistance && angle <= maxAngle)
				{
					yield return raycastHit.collider;
				}
			}
		}

		public Vector3 NearestNormal(float distancePower = 1f)
		{
			if (!HasHit)
				throw new Exception("There are no hits.");

			float maxDistance = MaxDistance();
			float minDistance = MinDistance();
			
			Vector3 normalSum = Vector3.zero;
			float weightSum = 0f;
			
			foreach (var raycastHit in Hits.Where(raycastHit => raycastHit.collider is not null))
			{
				float distance = raycastHit.distance - RaysIndent;

				float weight = 1f - Mathf.Pow((distance - minDistance) / (maxDistance - minDistance), distancePower);

				normalSum += raycastHit.normal * weight;

				weightSum += weight;
			}

			return Vector3.Normalize(normalSum / weightSum);
		}
		
		public float MinDistance()
		{
			if (!HasHit)
				throw new Exception("There are no hits.");
			
			float minDistance = Single.MaxValue;
			
			foreach (var raycastHit in Hits.Where(raycastHit => raycastHit.collider is not null))
			{
				float distance = raycastHit.distance - RaysIndent;

				if (distance < minDistance)
				{
					minDistance = distance;
				}
			}
			
			return minDistance;
		}
		
		public float MinDistanceWithAngleConstraint(float maxAngle)
		{
			if (!HasHit)
				throw new Exception("There are no hits.");
			
			float minDistance = Single.MaxValue;
			
			foreach (var raycastHit in Hits.Where(raycastHit => raycastHit.collider is not null))
			{
				float distance = raycastHit.distance - RaysIndent;
				float angle = Vector3.Angle(Vector3.up, raycastHit.normal);
				
				if (distance < minDistance && angle <= maxAngle)
				{
					minDistance = distance;
				}
			}
			
			return minDistance;
		}
		
		public float MaxDistance()
		{
			if (!HasHit)
				throw new Exception("There are no hits.");
			
			float maxDistance = Single.MinValue;
			
			foreach (var raycastHit in Hits.Where(raycastHit => raycastHit.collider is not null))
			{
				float distance = raycastHit.distance - RaysIndent;

				if (distance > maxDistance)
				{
					maxDistance = distance;
				}
			}
			
			return maxDistance;
		}
		
		public Vector3 AverageNormal()
		{
			if (!HasHit)
				throw new Exception("There are no hits.");
			
			Vector3 normalSum = Vector3.zero;
			float distanceSum = 0f;
			int hits = 0;
			
			foreach (var raycastHit in Hits.Where(raycastHit => raycastHit.collider is not null))
			{
				hits += 1;
				distanceSum += raycastHit.distance;
				normalSum += raycastHit.normal;
			}
			
			if (hits == 0)
				throw new Exception("There no ground hit.");

			return normalSum / hits;
		}
		
		public float AverageDistance()
		{
			if (!HasHit)
				throw new Exception("There are no hits.");
			
			Vector3 normalSum = Vector3.zero;
			float distanceSum = 0f;
			int hits = 0;
			
			foreach (var raycastHit in Hits.Where(raycastHit => raycastHit.collider is not null))
			{
				hits += 1;
				distanceSum += raycastHit.distance;
				normalSum += raycastHit.normal;
			}
			
			if (hits == 0)
				throw new Exception("There are no hits.");

			return distanceSum / hits - RaysIndent;
		}
	}
}