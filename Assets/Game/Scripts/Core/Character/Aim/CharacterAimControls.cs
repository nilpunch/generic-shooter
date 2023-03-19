using UnityEngine;

namespace SM.FPS
{
	public class CharacterAimControls : MonoBehaviour
	{
		[SerializeField] private CameraAim _cameraAim;
		[SerializeField, Range(0.01f, 10f)] private float _aimSensitivity = 0.01f;

		public void RotateAim(Vector2 delta)
		{
			_cameraAim.Rotate(delta * _aimSensitivity);
		}
	}
}