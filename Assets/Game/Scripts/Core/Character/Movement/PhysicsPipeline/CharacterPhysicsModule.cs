using UnityEngine;

namespace SM.FPS
{
	public abstract class CharacterPhysicsModule : MonoBehaviour
	{
		public abstract void Affect(IPhysics physics);
	}
}