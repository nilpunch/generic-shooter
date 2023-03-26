using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SM.FPS
{
	public class CharacterWeaponsInventory : MonoBehaviour
	{
		[SerializeField] private int _weaponsSlots = 2;
		[SerializeField] private CharacterAim _characterAim;
		[SerializeField] private CharacterHands _characterHands;

		[Header("Throwing weapon away")] [SerializeField]
		private float _throwForce = 10f;

		[SerializeField] private float _throwTorque = 3f;

		private readonly List<WeaponComponents> _weapons = new List<WeaponComponents>();
		private int _currentWeapon;

		public bool HaveFreeSlot => _weapons.Count < _weaponsSlots;

		protected Weapon CurrentWeapon => CurrentWeaponComponents?.Weapon;
		protected WeaponComponents CurrentWeaponComponents => _weapons.Count == 0 ? null : _weapons[_currentWeapon];

		private void Update()
		{
			CurrentWeapon?.Aim?.SetShootPositionAndDirection(_characterAim.WorldPosition, _characterAim.WorldDirection);
		}

		public void AddWeapon(WeaponComponents weaponComponentsPickup)
		{
			if (_weapons.Count == _weaponsSlots)
			{
				CurrentWeaponComponents.Weapon.MainFire?.Release();
				CurrentWeaponComponents.Weapon.AlterFire?.Release();
				CurrentWeaponComponents.HandledWeapon.ThrowAway(_characterAim.WorldDirection * _throwForce,
					Random.onUnitSphere * _throwTorque);

				_weapons[_currentWeapon] = weaponComponentsPickup;
			}
			else
			{
				_weapons.Add(weaponComponentsPickup);
				SwitchWeaponTo(_weapons.Count - 1);
			}

			weaponComponentsPickup.HandledWeapon.HandleBy(_characterHands);
		}

		public void ThrowAwayCurrentWeapon()
		{
			if (_weapons.Count == 0)
			{
				return;
			}

			CurrentWeaponComponents.Weapon.MainFire?.Release();
			CurrentWeaponComponents.Weapon.AlterFire?.Release();
			CurrentWeaponComponents.HandledWeapon.ThrowAway(_characterAim.WorldDirection * _throwForce,
				Random.onUnitSphere * _throwTorque);

			_weapons.RemoveAt(_currentWeapon);
			_currentWeapon = _weapons.Count - 1;
			CurrentWeaponComponents?.VisualWeapon.Show();
		}

		public void ScrollWeapons(int positions)
		{
			if (_weapons.Count == 0)
				return;

			var newWeapon = Mod(_currentWeapon + positions, _weapons.Count);

			SwitchWeaponTo(newWeapon);
		}

		private void SwitchWeaponTo(int newWeaponIndex)
		{
			if (_weapons.Count <= 1)
			{
				_currentWeapon = newWeaponIndex;
				return;
			}

			CurrentWeaponComponents.VisualWeapon.Hide();
			_currentWeapon = newWeaponIndex;
			CurrentWeaponComponents.VisualWeapon.Show();
		}

		public void PressWeaponMainPullTrigger()
		{
			CurrentWeapon?.Aim?.SetShootPositionAndDirection(_characterAim.WorldPosition, _characterAim.WorldDirection);
			CurrentWeapon?.MainFire?.Press();
		}

		public void ReleaseWeaponMainPullTrigger()
		{
			CurrentWeapon?.MainFire?.Release();
		}

		public void PressWeaponAlterPullTrigger()
		{
			CurrentWeapon?.Aim?.SetShootPositionAndDirection(_characterAim.WorldPosition, _characterAim.WorldDirection);
			CurrentWeapon?.AlterFire?.Press();
		}

		public void ReleaseWeaponAlterPullTrigger()
		{
			CurrentWeapon?.AlterFire?.Release();
		}

		public void ReloadCurrentWeapon()
		{
			CurrentWeapon?.Magazine?.Reload();
		}

		public void SwitchFiringMode()
		{
			CurrentWeapon?.FiringModeSwitch?.NextFiringMode();
		}

		private static int Mod(int a, int b)
		{
			int remainder = a % b;
			if ((b > 0 && remainder < 0) || (b < 0 && remainder > 0))
				return remainder + b;
			return remainder;
		}
	}
}