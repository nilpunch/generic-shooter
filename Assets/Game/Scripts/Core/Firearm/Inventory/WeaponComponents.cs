using JetBrains.Annotations;
using UnityEngine;

namespace SM.FPS
{
	public abstract class WeaponComponents : MonoBehaviour
	{
		[CanBeNull] public virtual ITrigger MainFire => null;
		[CanBeNull] public virtual ITrigger AlterFire => null;
		[CanBeNull] public virtual IWeaponMagazine Magazine => null;
		[CanBeNull] public virtual IFiringModeSwitch FiringModeSwitch => null;
	}
}