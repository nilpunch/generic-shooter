using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace SM.FPS
{
	public class FootRaycast : MonoBehaviour
	{
		[SerializeField] private Transform _checkOrigin;
		[SerializeField] private LayerMask _raycastMask;
		[SerializeField, Min(0f)] private float _raysDistance;
		[SerializeField, Min(0f)] private float _raysRadius;
		[SerializeField, Min(1)] private int _raysCount;
		
		[Header("Minor settings")]
		[SerializeField, Min(0f)] private float _raysIndent = 0.1f;
		[SerializeField, Min(0f)] private float _boundaryClip = 2;

		private Vector3[] _raysPositions;
		private NativeArray<RaycastCommand> _raycastInput;
		private NativeArray<RaycastHit> _raycastResult;

		public FootHit FootHit { get; private set; }
		
		private void Awake()
		{
			_raysPositions = SunflowerPointsDistribution(_raysCount, _boundaryClip).ToArray();
			_raycastInput = new NativeArray<RaycastCommand>(_raysCount, Allocator.Persistent);
			_raycastResult = new NativeArray<RaycastHit>(_raysCount, Allocator.Persistent);
		}

		private void OnDestroy()
		{
			_raycastInput.Dispose();
			_raycastResult.Dispose();
		}

		public void UpdateFootHit()
		{
			for (int i = 0; i < _raycastInput.Length; i++)
			{
				Vector3 from = _checkOrigin.position + _raysPositions[i] * _raysRadius + Vector3.up * _raysIndent;
				_raycastInput[i] = new RaycastCommand(from, Vector3.down, _raysDistance + _raysIndent, _raycastMask);
			}

			RaycastCommand.ScheduleBatch(_raycastInput, _raycastResult, 1).Complete();

			FootHit = new FootHit(_raycastResult.ToArray(), _raysIndent, _raysDistance);
		}

		private void OnDrawGizmosSelected()
		{
			foreach (var point in SunflowerPointsDistribution(_raysCount, _boundaryClip))
			{
				Vector3 from = _checkOrigin.position + point * _raysRadius + Vector3.up * _raysIndent;
				Gizmos.DrawLine(from, from + Vector3.down * (_raysDistance + _raysIndent));
			}
		}

		private static IEnumerable<Vector3> SunflowerPointsDistribution(int points, float boundaryClip)
		{
			float goldenRatio = (1f + Mathf.Sqrt(5f)) / 2f;
			float angleDelta = 2f * Mathf.PI / (goldenRatio * goldenRatio);
			int boundaryPoints = Mathf.RoundToInt(boundaryClip * Mathf.Sqrt(points));
			for (int i = 1; i < points + 1; i++) 
			{
				float radius = Radius(i, points, boundaryPoints);
				float angle = i * angleDelta;
				float x = Mathf.Cos(angle);
				float y = Mathf.Sin(angle);
				Vector3 point = new Vector3(x, 0, y) * radius;
				yield return point;
			}
			
			float Radius(int index, int pointsAmount, float onBoundary)
			{
				if (index > pointsAmount - onBoundary)
					return 1.0f;
			
				return Mathf.Sqrt(index - 0.5f) / Mathf.Sqrt(pointsAmount - (onBoundary + 1) / 2);
			}
		}
	}
}