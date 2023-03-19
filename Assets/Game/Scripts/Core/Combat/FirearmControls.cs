using UnityEngine;

namespace SM.FPS
{
	public class FirearmControls : MonoBehaviour
	{
		[SerializeField] private FirearmInventory _firearmInventory;
		
		public void UseKnife()
		{
			
		}

		public void UseGrenade()
		{
			
		}

		public void PressCurrentWeaponPullTrigger()
		{
			_firearmInventory.PressCurrentWeaponPullTrigger();
		}
		
		public void ReleaseCurrentWeaponPullTrigger()
		{
			_firearmInventory.ReleaseCurrentWeaponPullTrigger();
		}

		public void ReloadCurrentWeapon()
		{
			_firearmInventory.ReloadCurrentWeapon();
		}

		public void SwitchFiringMode()
		{
			_firearmInventory.SwitchFiringMode();
		}

		public void ScrollWeapons(int positions)
		{
			_firearmInventory.ScrollWeapons(positions);
		}
	}
}