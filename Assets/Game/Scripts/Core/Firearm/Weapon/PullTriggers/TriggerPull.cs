using UnityEngine;

namespace SM.FPS
{
	/// <summary>
	/// Determines fire mode of a gun and provide controls for fire.
	/// </summary>
	public abstract class TriggerPull : MonoBehaviour, ITrigger
	{
		public abstract void Press();
		public abstract void Release();
	}
}