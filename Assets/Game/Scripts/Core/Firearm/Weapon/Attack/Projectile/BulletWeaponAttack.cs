using Tools;
using UnityEngine;

namespace SM.FPS
{
	public class BulletWeaponAttack : WeaponAttack
	{
		[SerializeField] private Bullet _bulletPrefab;
		[SerializeField] private BulletShootFX _bulletShootFX;

		private Pool<Bullet> _bulletPool;

		private void Start()
		{
			_bulletPool = new Pool<Bullet>(new PrefabPoolFactory<Bullet>(_bulletPrefab));
		}

		public override void Attack(Vector3 position, Vector3 direction)
		{
			var bullet = _bulletPool.Get();
			bullet.SetPoolToSelfReturn(_bulletPool);
			bullet.Launch(position, direction);
			
			_bulletShootFX.PlayShootFx(bullet);
		}
	}
}