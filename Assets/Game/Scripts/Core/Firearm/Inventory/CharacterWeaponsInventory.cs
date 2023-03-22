using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace SM.FPS
{
	public class CharacterWeaponsInventory : MonoBehaviour
	{
		[SerializeField] private int _weaponsSlots = 2;
		[SerializeField] private CharacterAim _characterAim;
		[SerializeField] private CharacterHands _characterHands;
		
		[Header("Throwing weapon away")]
		[SerializeField] private float _throwForce = 10f;
		[SerializeField] private float _throwTorque = 3f;
		
		private readonly List<CharacterWeapon> _weapons = new List<CharacterWeapon>();
		private int _currentWeapon;

		public bool HaveFreeSlot => _weapons.Count < _weaponsSlots;
		
		protected WeaponComponents CurrentWeaponComponents => CurrentWeapon?.WeaponComponents;
		protected CharacterWeapon CurrentWeapon => _weapons.Count == 0 ? null : _weapons[_currentWeapon];

		public void AddWeapon(CharacterWeapon weaponPickup)
		{
			if (_weapons.Count == _weaponsSlots)
			{
				CurrentWeapon.WeaponComponents.MainFire?.Release();
				CurrentWeapon.WeaponComponents.AlterFire?.Release();
				CurrentWeapon.HandledWeapon.ThrowAway(_characterAim.WorldDirection * _throwForce, Random.onUnitSphere * _throwTorque);

				_weapons[_currentWeapon] = weaponPickup;
			}
			else
			{
				_weapons.Add(weaponPickup);
				_currentWeapon = _weapons.Count - 1;
			}
			
			weaponPickup.HandledWeapon.HandleBy(_characterHands);
			weaponPickup.Aim.Aim(_characterAim);
		}

		public void ThrowAwayCurrentWeapon()
		{
			if (_weapons.Count == 0)
			{
				return;
			}

			CurrentWeapon.WeaponComponents.MainFire?.Release();
			CurrentWeapon.WeaponComponents.AlterFire?.Release();
			CurrentWeapon.HandledWeapon.ThrowAway(_characterAim.WorldDirection * _throwForce, Random.onUnitSphere * _throwTorque);

			_weapons.RemoveAt(_currentWeapon);
			_currentWeapon = _weapons.Count - 1;
		}
		
		public void ScrollWeapons(int positions)
		{
			if (_weapons.Count == 0)
				return;
			
			_currentWeapon = (_currentWeapon + positions) % _weapons.Count;
		}

		public void PressWeaponMainPullTrigger()
		{
			CurrentWeaponComponents?.MainFire?.Press();
		}

		public void ReleaseWeaponMainPullTrigger()
		{
			CurrentWeaponComponents?.MainFire?.Release();
		}

		public void PressWeaponAlterPullTrigger()
		{
			CurrentWeaponComponents?.AlterFire?.Press();
		}

		public void ReleaseWeaponAlterPullTrigger()
		{
			CurrentWeaponComponents?.AlterFire?.Release();
		}

		public void ReloadCurrentWeapon()
		{
			CurrentWeaponComponents?.Magazine?.Reload();
		}

		public void SwitchFiringMode()
		{
			CurrentWeaponComponents?.FiringModeSwitch?.NextFiringMode();
		}
	}
}