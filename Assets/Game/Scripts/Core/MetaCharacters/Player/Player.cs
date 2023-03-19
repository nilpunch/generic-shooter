using UnityEngine;

namespace SM.FPS
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private CharacterMovementControls _characterMovementControls;
		[SerializeField] private CharacterAimControls _characterAimControls;
		
		private PlayerInput _playerInput;

		private void Awake()
		{
			Cursor.visible = false;
			
			_playerInput = new PlayerInput();
			_playerInput.Enable();
		}

		private void Update()
		{
			UpdateMovementControls();

			UpdateAimControls();
		}

		private void UpdateMovementControls()
		{
			Vector2 moveInput = _playerInput.Character.Move.ReadValue<Vector2>();
			_characterMovementControls.Move(new Vector3(moveInput.x, 0f, moveInput.y));

			if (_playerInput.Character.Jump.WasPerformedThisFrame())
			{
				if (_playerInput.Character.Jump.IsPressed())
				{
					_characterMovementControls.Jump();
				}
				else
				{
					_characterMovementControls.Fall();
				}
			}
		}

		private void UpdateAimControls()
		{
			Vector2 aimDeltaInput = _playerInput.Character.Aim.ReadValue<Vector2>();
			_characterAimControls.RotateAim(aimDeltaInput);
		}
	}
}