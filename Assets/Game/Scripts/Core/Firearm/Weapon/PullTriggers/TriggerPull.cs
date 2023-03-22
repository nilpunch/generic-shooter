using UnityEngine;

namespace SM.FPS
{
	/// <summary>
	/// For dummies it is "Курок".
	/// </summary>
	public abstract class TriggerPull : MonoBehaviour, ITrigger
	{
		public abstract void Press();
		public abstract void Release();
	}
}