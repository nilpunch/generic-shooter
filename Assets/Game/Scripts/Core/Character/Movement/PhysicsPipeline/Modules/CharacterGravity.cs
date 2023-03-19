using UnityEngine;

namespace SM.FPS
{
	public class CharacterGravity : CharacterPhysicsModule
	{
		[field: SerializeField] public Vector3 Gravity { get; private set; } = new Vector3(0f, -78f, 0f);
		[field: SerializeField] public Vector3 WorldUp { get; private set; } = new Vector3(0f, 1f, 0f);
		
		public float GravityMagnitude => Gravity.magnitude;

		public Vector3 Affection => Gravity * Time.deltaTime;

		public override void Affect(IPhysics physics)
		{
			physics.AddForce(Affection);
		}
	}
}