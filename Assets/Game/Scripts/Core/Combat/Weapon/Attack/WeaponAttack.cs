using UnityEngine;

namespace SM.FPS
{
	/// <summary>
	/// Determines actual firing.
	/// </summary>
	public abstract class WeaponAttack : MonoBehaviour
	{
		public abstract void Attack(Vector3 position, Vector3 direction);
	}
}