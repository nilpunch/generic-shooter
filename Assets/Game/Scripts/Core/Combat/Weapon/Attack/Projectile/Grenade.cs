using UnityEngine;

namespace SM.FPS
{
	public class Grenade : Projectile
	{
		[SerializeField] private float _damage = 10f;
		[SerializeField] private float _throwSpeed = 4f;
		[SerializeField] private float _blastRadius = 5f;
		[SerializeField] private float _fuseTime = 4f;

		[Header("Rigidbody settings")]
		[SerializeField] private Rigidbody _rigidbody;
		
		[Header("Raycast settings")]
		[SerializeField] private LayerMask _raycastLayers;

		private float _launchTime;

		public override void Launch(Vector3 position, Vector3 direction)
		{
			_launchTime = Time.time;

			gameObject.SetActive(true);

			transform.position = position;
			transform.rotation = Quaternion.identity;
			_rigidbody.position = position;
			_rigidbody.velocity = direction * _throwSpeed;
			_rigidbody.angularVelocity = Vector3.zero;
		}

		private void Update()
		{
			if (_launchTime + _fuseTime < Time.time)
			{
				ExplosionUtility.RaycastBlast(transform.position, _blastRadius, _raycastLayers, _damage);
				SelfDestruct();
			}
		}

		private void SelfDestruct()
		{
			gameObject.SetActive(false);
			PoolReturn.Return(this);
		}
	}
}