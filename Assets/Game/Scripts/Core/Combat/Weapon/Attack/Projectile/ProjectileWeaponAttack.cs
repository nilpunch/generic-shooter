using Tools;
using UnityEngine;

namespace SM.FPS
{
	public class ProjectileWeaponAttack : WeaponAttack
	{
		[SerializeField] private Bullet _bulletPrefab;
		
		private Pool<Projectile> _projectilePool;

		private void Awake()
		{
			_projectilePool = new Pool<Projectile>(new PrefabPoolFactory<Bullet>(_bulletPrefab));
		}

		public override void Attack(Vector3 position, Vector3 direction)
		{
			var projectile = _projectilePool.Get();
			projectile.SetPoolToSelfReturn(_projectilePool);
			projectile.Launch(position, direction);
		}
	}
}