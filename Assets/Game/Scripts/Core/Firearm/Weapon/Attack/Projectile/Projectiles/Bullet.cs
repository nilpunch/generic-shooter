using Tools;
using UnityEngine;

namespace SM.FPS
{
	public class Bullet : MonoBehaviour
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
		private IPoolReturn<Bullet> _poolReturn;
		
		public bool IsAlive => gameObject.activeSelf;
		public Vector3 DestroyPosition { get; private set; }

		public void Launch(Vector3 position, Vector3 direction)
		{
			_previousPosition = position;
			_velocity = direction * _speed;
			_launchTime = Time.time;

			gameObject.SetActive(true);
			
			transform.position = position;
			transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
		}

		public void SetPoolToSelfReturn(IPoolReturn<Bullet> poolReturn)
		{
			_poolReturn = poolReturn;
		}

		private void FixedUpdate()
		{
			if (_launchTime + _lifetime < Time.time)
			{
				DestroySelf();
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

				transform.position = _previousPosition + ray.normalized * hitInfo.distance;
				DestroySelf();
			}

			_previousPosition = transform.position;
			transform.rotation = Quaternion.LookRotation(_velocity, Vector3.up);
		}

		private void DestroySelf()
		{
			DestroyPosition = transform.position;
			gameObject.SetActive(false);
			_poolReturn.Return(this);
		}
	}
}