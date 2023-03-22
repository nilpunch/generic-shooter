using UnityEngine;

namespace SM.FPS
{
	public abstract class CharacterAim : MonoBehaviour
	{
		public abstract Vector3 WorldPosition { get; }
		public abstract Vector3 WorldDirection { get; }
	}
}