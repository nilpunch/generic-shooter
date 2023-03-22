using Tools;
using UnityEngine;

namespace SM.FPS
{
	public abstract class Projectile : MonoBehaviour
	{
		protected IPoolReturn<Projectile> PoolReturn { get; private set; }
		
		public void SetPoolToSelfReturn(IPoolReturn<Projectile> poolReturn)
		{
			PoolReturn = poolReturn;
		}

		public abstract void Launch(Vector3 position, Vector3 direction);
	}
}