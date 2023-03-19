using UnityEngine;

namespace SM.FPS
{
	public class MovingPlatform : Platform, IMovingPlatform
	{
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private Transform[] _waypoints;
		[SerializeField] private float _moveSpeed;
		[SerializeField] private float _waitTime;
		[SerializeField] private bool _loop;

		private int _currentWaypoint;
		private int _increment = 1;
		private float _elapsedWaitTime;

		private Vector3 _currentVelocity;
		
		public override void ManualUpdate()
		{
			Vector3 waypointPosition = _waypoints[_currentWaypoint].position;
			Vector3 lastPosition = _rigidbody.position;

			bool destinationReached = Vector3.Distance(waypointPosition, lastPosition) < 0.001f;

			Vector3 newPosition = Vector3.MoveTowards(lastPosition, waypointPosition, Time.deltaTime * _moveSpeed);
			
			_currentVelocity = (newPosition - lastPosition) / Time.deltaTime;
			
			_rigidbody.MovePosition(newPosition);

			if (!destinationReached)
				return;
			
			_elapsedWaitTime += Time.deltaTime;
			
			if (_elapsedWaitTime > _waitTime)
			{
				_elapsedWaitTime = 0f;
				PeekNextWaypoint();
			}

			void PeekNextWaypoint()
			{
				if (_loop)
				{
					_currentWaypoint += 1;
				}
				else
				{
					_currentWaypoint += _increment;
					if (_currentWaypoint == _waypoints.Length - 1 || _currentWaypoint == 0)
						_increment = -_increment;
				}
				
				_currentWaypoint %= _waypoints.Length;
			}
		}

		public void ForwardVelocityTo(IPhysics physics)
		{
			physics.AddInheritedForce(_currentVelocity);
		}
	}
}