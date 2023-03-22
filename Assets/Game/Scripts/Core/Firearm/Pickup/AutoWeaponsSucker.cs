using UnityEngine;

namespace SM.FPS
{
	public class AutoWeaponsSucker : MonoBehaviour
	{
		[SerializeField] private CharacterWeaponsInventory _characterWeaponsInventory;

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<WeaponPickup>(out var weaponPickup))
			{
				if (weaponPickup.CanBePickedUp && _characterWeaponsInventory.HaveFreeSlot)
				{
					_characterWeaponsInventory.AddWeapon(weaponPickup.CharacterWeapon);
				}
			}
		}
	}
}