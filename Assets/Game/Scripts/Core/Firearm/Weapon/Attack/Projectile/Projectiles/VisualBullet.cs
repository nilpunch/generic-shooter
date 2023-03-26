using System.Collections;
using Tools;
using UnityEngine;

namespace SM.FPS
{
	public class VisualBullet : MonoBehaviour
	{
		[SerializeField] private float _speed = 10f;
		[SerializeField] private float _downforce = 0.5f;
		[SerializeField] private ParticleSystem _trailFX;
		[SerializeField] private ParticleSystem _hitFX;

		private Vector3 _velocity;
		private IPoolReturn<VisualBullet> _poolReturn;
		private Bullet _realBullet;

		private bool _destroyed;

		public void SetPoolToSelfReturn(IPoolReturn<VisualBullet> poolReturn)
		{
			_poolReturn = poolReturn;
		}

		public void Launch(Vector3 position, Vector3 direction, Bullet realBullet)
		{
			_velocity = direction * _speed;

			_realBullet = realBullet;
			_destroyed = false;
			gameObject.SetActive(true);
			_hitFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
			_trailFX.Play();
			
			transform.position = position;
			transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
		}

		private void Update()
		{
			if (_destroyed)
				return;
			
			if (!_realBullet.IsAlive)
			{
				DestroySelf();
				return;
			}
			
			Vector3 acceleration = Vector3.down * _downforce;
			
			// Verlet integration
			transform.position += _velocity * Time.deltaTime + 0.5f * acceleration * Time.deltaTime * Time.deltaTime; 
			_velocity += acceleration * Time.deltaTime; 

			transform.rotation = Quaternion.LookRotation(_velocity, Vector3.up);
		}

		private void DestroySelf()
		{
			StartCoroutine(SelfDestroyCoroutine());
		}

		private IEnumerator SelfDestroyCoroutine()
		{
			_destroyed = true;
			transform.position = _realBullet.DestroyPosition;
			
			_trailFX.Stop(true, ParticleSystemStopBehavior.StopEmitting);
			_hitFX.Play();
			yield return new WaitForSeconds(Mathf.Max(_trailFX.main.startLifetime.constant, _hitFX.main.startLifetime.constant));
			gameObject.SetActive(false);
			_poolReturn.Return(this);
		}
	}
}