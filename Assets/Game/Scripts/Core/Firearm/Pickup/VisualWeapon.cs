using UnityEngine;

namespace SM.FPS
{
	public class VisualWeapon : MonoBehaviour
	{
		[SerializeField] private Renderer[] _renderers;
		
		public void Show()
		{
			foreach (var renderer in _renderers)
			{
				renderer.enabled = true;
			}
		}

		public void Hide()
		{
			foreach (var renderer in _renderers)
			{
				renderer.enabled = false;
			}
		}
	}
}