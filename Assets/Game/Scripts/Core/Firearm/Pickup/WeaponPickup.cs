using UnityEngine;

namespace SM.FPS
{
	public class WeaponPickup : MonoBehaviour
	{
		[SerializeField] private WeaponComponents _weaponComponents;

		public WeaponComponents WeaponComponents => _weaponComponents;

		public bool CanBePickedUp => _weaponComponents.HandledWeapon.CanBePickedUp;
	}
}