using UnityEngine;

namespace SM.FPS
{
	public class HandledWeapon : MonoBehaviour
	{
		[SerializeField] private Transform _handlePivot;
		[SerializeField] private Transform _weaponRoot;
		[SerializeField] private Rigidbody _weaponRigidbody;
		[SerializeField] private float _pickupCooldownAfterThrowingAway = 3f;

		private CharacterHands _characterHands;
		private float _lastThrowAwayTime = float.MinValue;

		public bool IsHandled => _characterHands != null;

		public bool CanBePickedUp => !IsHandled && _lastThrowAwayTime + _pickupCooldownAfterThrowingAway < Time.time;

		public void HandleBy(CharacterHands characterHands)
		{
			_characterHands = characterHands;
			_weaponRigidbody.isKinematic = true;
			_weaponRigidbody.detectCollisions = false;
			
			_weaponRoot.SetParent(_characterHands.WeaponHandlePivot, false);
			_weaponRoot.localRotation = _handlePivot.localRotation;
			_weaponRoot.localPosition = _weaponRoot.InverseTransformPoint(_handlePivot.position);
		}

		public void ThrowAway(Vector3 force, Vector3 torque)
		{
			_characterHands = null;
			_lastThrowAwayTime = Time.time;
			_weaponRoot.SetParent(null, true);
			_weaponRigidbody.isKinematic = false;
			_weaponRigidbody.detectCollisions = true;
			_weaponRigidbody.AddForce(force, ForceMode.Impulse);
			_weaponRigidbody.AddTorque(torque, ForceMode.Impulse);
		}
	}
}