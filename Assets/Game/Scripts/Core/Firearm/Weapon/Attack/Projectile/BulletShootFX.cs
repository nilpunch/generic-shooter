using Tools;
using UnityEngine;

namespace SM.FPS
{
	public class BulletShootFX : MonoBehaviour
	{
		[SerializeField] private VisualBullet _visualPrefab;
		[SerializeField] private Transform _visualShootOrigin;
		
		private Pool<VisualBullet> _visualBulletPool;

		private void Start()
		{
			_visualBulletPool = new Pool<VisualBullet>(new PrefabPoolFactory<VisualBullet>(_visualPrefab));
		}

		public void PlayShootFx(Bullet realBullet)
		{
			var visualBullet = _visualBulletPool.Get();
			visualBullet.SetPoolToSelfReturn(_visualBulletPool);
			visualBullet.Launch(_visualShootOrigin.position, _visualShootOrigin.forward, realBullet);
		}
	}
}