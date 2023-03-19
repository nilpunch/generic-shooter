using UnityEngine;

namespace SM.FPS
{
	public abstract class DamageTarget : MonoBehaviour
	{
		public abstract void TakeDamage(float damage);
	}
}