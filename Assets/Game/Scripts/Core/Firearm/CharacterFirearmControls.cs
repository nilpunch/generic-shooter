using UnityEngine;

namespace SM.FPS
{
	public class CharacterFirearmControls : MonoBehaviour
	{
		[SerializeField] private CharacterWeaponsInventory _characterWeaponsInventory;
		[SerializeField] private SecondaryEquipment _secondaryEquipment;
		
		public void UseKnife()
		{
			_secondaryEquipment.UseKnife();
		}

		public void UseGrenade()
		{
			_secondaryEquipment.UseGrenade();
		}

		public void PressWeaponMainPullTrigger()
		{
			_characterWeaponsInventory.PressWeaponMainPullTrigger();
		}
		
		public void ReleaseWeaponMainPullTrigger()
		{
			_characterWeaponsInventory.ReleaseWeaponMainPullTrigger();
		}
		
		public void PressWeaponAlterPullTrigger()
		{
			_characterWeaponsInventory.PressWeaponAlterPullTrigger();
		}
		
		public void ReleaseWeaponAlterPullTrigger()
		{
			_characterWeaponsInventory.ReleaseWeaponAlterPullTrigger();
		}

		public void ReloadCurrentWeapon()
		{
			_characterWeaponsInventory.ReloadCurrentWeapon();
		}

		public void SwitchFiringMode()
		{
			_characterWeaponsInventory.SwitchFiringMode();
		}
		
		public void ThrowAwayCurrentWeapon()
		{
			_characterWeaponsInventory.ThrowAwayCurrentWeapon();
		}

		public void ScrollWeapons(int positions)
		{
			_characterWeaponsInventory.ScrollWeapons(positions);
		}
	}
}