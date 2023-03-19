using System;
using UnityEngine;

namespace SM.FPS
{
	public class PunchPad : MonoBehaviour
	{
		[SerializeField] private float _punchForce;
		[SerializeField] private float _punchDelay = 0.1f;

		private float _lastPunchTime = Single.MinValue;
		
		private void OnCollisionStay(Collision other)
		{
			if (other.collider.TryGetComponent<IPunchable>(out var punchable))
			{
				if (_lastPunchTime + _punchDelay < Time.time)
				{
					_lastPunchTime = Time.time;
					punchable.Punch(-other.GetContact(0).normal * _punchForce);
				}
			}
		}
	}
}