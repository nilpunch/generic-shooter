using UnityEngine;

namespace SM.FPS
{
	/// <summary>
	/// Determines firing mode of a weapon.
	/// </summary>
	public abstract class TriggerPull : MonoBehaviour
	{
		public abstract void Press();
		public abstract void Release();
	}
}