using JetBrains.Annotations;

namespace SM.FPS
{
	public class Weapon
	{
		public Weapon(TriggerPull triggerPull, [CanBeNull] IWeaponMagazine magazine, [CanBeNull] IFiringModeSwitch firingModeSwitch)
		{
			TriggerPull = triggerPull;
			Magazine = magazine;
			FiringModeSwitch = firingModeSwitch;
		}

		public TriggerPull TriggerPull { get; }
		[CanBeNull] public IWeaponMagazine Magazine { get; }
		[CanBeNull] public IFiringModeSwitch FiringModeSwitch { get; }
	}
}