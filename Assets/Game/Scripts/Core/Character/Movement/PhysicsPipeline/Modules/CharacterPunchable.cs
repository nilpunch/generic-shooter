using UnityEngine;

namespace SM.FPS
{
	public class CharacterPunchable : CharacterPhysicsModule, IPunchable
	{
		[SerializeField, Range(0f, 1f)] private float _verticalMovementAffection = 0.25f;
		
		private Vector3 _accumulatedPunch;
		
		public override void Affect(IPhysics physics)
		{
			physics.AddForce(Vector3.Scale(_accumulatedPunch, new Vector3(1f, _verticalMovementAffection, 1f)));
			_accumulatedPunch = Vector3.zero;
		}

		public void Punch(Vector3 force)
		{
			_accumulatedPunch += force;
		}
	}
}