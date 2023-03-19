using UnityEngine;

namespace SM.FPS
{
	public class SingleActionTriggerPull : TriggerPull
	{
		[SerializeField] private WeaponAimedAttack _weaponAimedAttack;
		[SerializeField] private float _delayBetweenAttacks = 0.1f;

		private bool _pressed;
		private float _lastShotTime;

		public override void Press()
		{
			bool delayBetweenShots = _lastShotTime + _delayBetweenAttacks < Time.time;
			
			if (_pressed || delayBetweenShots)
				return;

			_pressed = true;
			_lastShotTime = Time.time;
			_weaponAimedAttack.AimedAttack();
		}

		public override void Release()
		{
			_pressed = false;
		}
	}
}