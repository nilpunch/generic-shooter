using UnityEngine;

namespace SM.FPS
{
	public class CharacterAimControls : MonoBehaviour
	{
		[SerializeField] private CharacterCameraAim _characterCameraAim;
		[SerializeField, Range(0.01f, 10f)] private float _aimSensitivity = 0.25f;

		public void RotateAim(Vector2 delta)
		{
			_characterCameraAim.RotateAim(delta * _aimSensitivity);
		}
	}
}