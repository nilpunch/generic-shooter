using UnityEngine;

namespace SM.FPS
{
	public class SemiAutoTriggerPull : TriggerPull
	{
		[SerializeField] private WeaponAimedAttack _weaponAimedAttack;
		[SerializeField] private int _shotsPerQuery = 3;
		[SerializeField] private float _delayBetweenAttacks = 0.1f;
		[SerializeField] private float _delayBetweenQueries = 0.3f;

		private bool _pressed;
		private bool _firing;
		private int _remainingShots;

		private float _lastShotTime;

		private void Update()
		{
			if (_remainingShots == 0)
				return;

			if (_lastShotTime + _delayBetweenAttacks < Time.time)
			{
				_lastShotTime += _delayBetweenAttacks;
				_remainingShots -= 1;
				_weaponAimedAttack.AimedAttack();
			}
		}
		
		public override void Press()
		{
			bool stillFiring = _remainingShots != 0;
			bool delayBetweenQueries = _lastShotTime + _delayBetweenQueries < Time.time;
			
			if (_pressed || stillFiring || delayBetweenQueries)
				return;

			_pressed = true;
			_lastShotTime = Time.time;

			_remainingShots = _shotsPerQuery - 1; // Minus 1 since we attacking instantly
			_weaponAimedAttack.AimedAttack();
		}

		public override void Release()
		{
			_pressed = false;
		}
	}
}