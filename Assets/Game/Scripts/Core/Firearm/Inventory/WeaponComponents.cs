using UnityEngine;

namespace SM.FPS
{
	/// <summary>
	/// Root for assembled weapon.
	/// </summary>
	public class WeaponComponents : MonoBehaviour
	{
		[field: SerializeField] public Weapon Weapon { get; private set; }
		[field: SerializeField] public HandledWeapon HandledWeapon { get; private set; }
		[field: SerializeField] public VisualWeapon VisualWeapon { get; private set; }
	}
}