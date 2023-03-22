using UnityEngine;

namespace SM.FPS
{
	public class CharacterFirearmControls : MonoBehaviour
	{
		[SerializeField] private CharacterWeaponsInventory _weaponsInventory;
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
			_weaponsInventory.PressWeaponMainPullTrigger();
		}
		
		public void ReleaseWeaponMainPullTrigger()
		{
			_weaponsInventory.ReleaseWeaponMainPullTrigger();
		}
		
		public void PressWeaponAlterPullTrigger()
		{
			_weaponsInventory.PressWeaponAlterPullTrigger();
		}
		
		public void ReleaseWeaponAlterPullTrigger()
		{
			_weaponsInventory.ReleaseWeaponAlterPullTrigger();
		}

		public void ReloadCurrentWeapon()
		{
			_weaponsInventory.ReloadCurrentWeapon();
		}

		public void SwitchFiringMode()
		{
			_weaponsInventory.SwitchFiringMode();
		}
		
		public void ThrowAwayCurrentWeapon()
		{
			_weaponsInventory.ThrowAwayCurrentWeapon();
		}

		public void ScrollWeapons(int positions)
		{
			_weaponsInventory.ScrollWeapons(positions);
		}
	}
}