using System;
using UnityEngine;

namespace SM.FPS
{
	public class MagazineWeaponAttack : WeaponAttack, IWeaponMagazine
	{
		[SerializeField] private WeaponAttack _weaponAttack;
		[SerializeField] private int _magazineCapacity = 10;
		[SerializeField] private int _maxAmmo = 60;
		
		private int _ammoInMagazine = 0;
		private int _ammoAmount = 0;
		
		public override void Attack(Vector3 position, Vector3 direction)
		{
			if (_ammoInMagazine == 0)
				return;

			_ammoInMagazine -= 1;
			
			_weaponAttack.Attack(position, direction);
		}

		public void Reload()
		{
			int needToReplenish = _magazineCapacity - _ammoInMagazine;
			int canReplenish = Mathf.Min(_ammoAmount, needToReplenish);

			_ammoAmount -= canReplenish;
			_ammoInMagazine += canReplenish;
		}

		public void ChargeAmmo(int amount)
		{
			if (amount <= 0)
				throw new ArgumentOutOfRangeException(nameof(amount));

			var canCharge = _maxAmmo - _ammoAmount;
			_ammoAmount += Mathf.Min(amount, canCharge);
		}
	}
}