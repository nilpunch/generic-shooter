using UnityEngine;

namespace SM.FPS
{
	public abstract class WeaponPickup : MonoBehaviour
	{
		public abstract Weapon Weapon { get; }
	}
}