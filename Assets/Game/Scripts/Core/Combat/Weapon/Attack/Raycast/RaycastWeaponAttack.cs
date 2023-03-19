using UnityEngine;

namespace SM.FPS
{
	public class RaycastWeaponAttack : WeaponAttack
	{
		[SerializeField] private float _damage = 1f;
		[SerializeField] private float _maxDistance = 100f;

		[Header("Raycast settings")]
		[SerializeField] private LayerMask _raycastLayers;
		[SerializeField] private float _projectileRadius = 0.2f;
		
		public override void Attack(Vector3 position, Vector3 direction)
		{
			if (Physics.SphereCast(position, _projectileRadius, direction, out var hitInfo,
				    _maxDistance, _raycastLayers))
			{
				if (hitInfo.collider.TryGetComponent<DamageTarget>(out var damageTarget))
				{
					damageTarget.TakeDamage(_damage);
				}
			}
		}
	}
}