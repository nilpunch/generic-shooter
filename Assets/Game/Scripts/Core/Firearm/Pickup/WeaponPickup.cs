using System;
using UnityEngine;

namespace SM.FPS
{
	public class WeaponPickup : MonoBehaviour
	{
		[SerializeField] private CharacterWeapon _characterWeapon;

		public CharacterWeapon CharacterWeapon => _characterWeapon;

		public bool CanBePickedUp => _characterWeapon.HandledWeapon.CanBePickedUp;
	}
}