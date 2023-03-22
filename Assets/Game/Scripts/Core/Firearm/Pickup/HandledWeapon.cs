using System;
using UnityEngine;

namespace SM.FPS
{
	public class HandledWeapon : MonoBehaviour
	{
		[SerializeField] private Transform _handlePosition;
		[SerializeField] private Transform _weaponRoot;
		[SerializeField] private Rigidbody _weaponRigidbody;
		[SerializeField] private float _pickupCooldownAfterThrowingAway = 3f;

		private CharacterHands _characterHands;
		private float _lastThrowAwayTime;

		public bool IsHandled => _characterHands != null;

		public bool CanBePickedUp => !IsHandled && _lastThrowAwayTime + _pickupCooldownAfterThrowingAway < Time.time;

		private void LateUpdate()
		{
			if (!IsHandled)
				return;

			_weaponRoot.position = _characterHands.HandlePivot.position;
			_weaponRoot.rotation = _characterHands.HandlePivot.rotation;

			_weaponRoot.position -= _weaponRoot.InverseTransformPoint(_handlePosition.position);
		}

		public void HandleBy(CharacterHands characterHands)
		{
			_characterHands = characterHands;
			_weaponRigidbody.isKinematic = true;
			_weaponRigidbody.detectCollisions = false;
		}

		public void ThrowAway(Vector3 force, Vector3 torque)
		{
			_characterHands = null;
			_lastThrowAwayTime = Time.time;
			_weaponRigidbody.isKinematic = false;
			_weaponRigidbody.detectCollisions = true;
			_weaponRigidbody.AddForce(force, ForceMode.Impulse);
			_weaponRigidbody.AddTorque(torque, ForceMode.Impulse);
		}
	}
}