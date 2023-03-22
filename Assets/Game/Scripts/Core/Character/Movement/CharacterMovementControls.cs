using UnityEngine;

namespace SM.FPS
{
	public class CharacterMovementControls : MonoBehaviour
	{
		[SerializeField] private CharacterVerticalMovement _characterVerticalMovement;
		[SerializeField] private CharacterHorizontalMovement _characterHorizontalMovement;
		[SerializeField] private CharacterCameraAim _characterCameraAim;

		public void Jump()
		{
			_characterVerticalMovement.Jump();
		}
		
		public void Fall()
		{
			_characterVerticalMovement.Fall();
		}

		public void Move(Vector3 direction)
		{
			Vector3 directionRelativeToAim = _characterCameraAim.YawRotation * direction;
			
			_characterHorizontalMovement.Move(directionRelativeToAim);
		}
	}
}