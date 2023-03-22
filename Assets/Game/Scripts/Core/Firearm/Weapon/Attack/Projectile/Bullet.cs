using UnityEngine;

namespace SM.FPS
{
	public class Bullet : Projectile
	{
		[SerializeField] private float _damage = 1f;
		[SerializeField] private float _speed = 10f;
		[SerializeField] private float _downforce = 0.5f;
		[SerializeField] private float _lifetime = 3f;

		[Header("Raycast settings")]
		[SerializeField] private LayerMask _raycastLayers;
		[SerializeField] private float _projectileRadius = 0.2f;

		private Vector3 _velocity;
		private Vector3 _previousPosition;
		private float _launchTime;

		public override void Launch(Vector3 position, Vector3 direction)
		{
			_previousPosition = position;
			_velocity = direction * _speed;
			_launchTime = Time.time;

			gameObject.SetActive(true);
			
			transform.position = position;
			transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
		}

		private void FixedUpdate()
		{
			if (_launchTime + _lifetime < Time.time)
			{
				SelfDestruct();
				return;
			}
			
			Vector3 acceleration = Vector3.down * _downforce;
			
			// Verlet integration
			transform.position += _velocity * Time.deltaTime + 0.5f * acceleration * Time.deltaTime * Time.deltaTime; 
			_velocity += acceleration * Time.deltaTime; 
			
			// Raycast from previous position to current to prevent going through walls on high speed
			Vector3 ray = transform.position - _previousPosition;
			if (Physics.SphereCast(_previousPosition, _projectileRadius, ray, out var hitInfo,
				    ray.magnitude, _raycastLayers))
			{
				if (hitInfo.collider.TryGetComponent<DamageTarget>(out var damageTarget))
				{
					damageTarget.TakeDamage(_damage);
				}

				SelfDestruct();
			}

			_previousPosition = transform.position;
			transform.rotation = Quaternion.LookRotation(_velocity, Vector3.up);
		}

		private void SelfDestruct()
		{
			gameObject.SetActive(false);
			PoolReturn.Return(this);
		}
	}
}