using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace SM.FPS
{
	public class FirearmInventory : MonoBehaviour
	{
		[SerializeField] private int _weaponsSlots = 2;

		private readonly List<Weapon> _weapons = new List<Weapon>();
		private int _currentWeapon;

		public bool HaveFreeSlot => _weapons.Count < _weaponsSlots;
		
		[CanBeNull] private Weapon CurrentWeapon => _weapons.Count == 0 ? null : _weapons[_currentWeapon];

		public void AddWeapon(Weapon weapon)
		{
			if (_weapons.Count == _weaponsSlots)
			{
				_weapons[_currentWeapon] = weapon;
			}
			else
			{
				// If have free weapon slot then we add new weapon and set it as
				_weapons.Add(weapon);
				_currentWeapon = _weapons.Count - 1;
			}
		}

		public void UseKnife()
		{
			
		}

		public void UseGrenade()
		{
			
		}

		public void PressCurrentWeaponPullTrigger()
		{
			CurrentWeapon?.TriggerPull.Press();
		}
		
		public void ReleaseCurrentWeaponPullTrigger()
		{
			CurrentWeapon?.TriggerPull.Release();
		}
		
		public void ReloadCurrentWeapon()
		{
			CurrentWeapon?.Magazine?.Reload();
		}

		public void SwitchFiringMode()
		{
			CurrentWeapon?.FiringModeSwitch?.NextFiringMode();
		}

		public void ScrollWeapons(int positions)
		{
			_currentWeapon = (_currentWeapon + positions) % _weapons.Count;
		}
	}
}