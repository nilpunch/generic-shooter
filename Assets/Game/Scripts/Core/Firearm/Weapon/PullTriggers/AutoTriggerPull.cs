using UnityEngine;

namespace SM.FPS
{
	public class AutoTriggerPull : TriggerPull
	{
		[SerializeField] private WeaponAimedAttack _weaponAimedAttack;
		[SerializeField] private float _delayBetweenAttacks = 0.1f;

		private bool _pressed;
		private float _lastShotTime;

		private void Update()
		{
			if (!_pressed)
				return;
			
			if (_lastShotTime + _delayBetweenAttacks < Time.time)
			{
				_lastShotTime += _delayBetweenAttacks;
				_weaponAimedAttack.AimedAttack();
			}
		}
		
		public override void Press()
		{
			if (_pressed)
				return;

			_pressed = true;

			if (_lastShotTime + _delayBetweenAttacks < Time.time)
			{
				_lastShotTime = Time.time;
				_weaponAimedAttack.AimedAttack();
			}
		}

		public override void Release()
		{
			_pressed = false;
		}
	}
}