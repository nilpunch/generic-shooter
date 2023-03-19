using UnityEngine;

namespace SM.FPS
{
	public class WeaponVacuumCleaner : MonoBehaviour
	{
		[SerializeField] private FirearmInventory _firearmInventory;

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<WeaponPickup>(out var weaponPickup))
			{
				if (_firearmInventory.HaveFreeSlot)
				{
					_firearmInventory.AddWeapon(weaponPickup.Weapon);
				}
			}
		}
	}
}