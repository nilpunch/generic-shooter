using UnityEngine;

namespace SM.FPS
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private CharacterMovementControls _characterMovementControls;
		[SerializeField] private CharacterAimControls _characterAimControls;
		[SerializeField] private CharacterFirearmControls _characterFirearmControls;
		
		private PlayerInput _playerInput;

		private void Awake()
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			
			_playerInput = new PlayerInput();
			_playerInput.Enable();
		}

		private void Update()
		{
			UpdateMovementControls();

			UpdateAimControls();
			
			UpdateFirearmControls();
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

		private void UpdateFirearmControls()
		{
			if (_playerInput.Weapons.MainFire.WasPressedThisFrame())
			{
				_characterFirearmControls.PressWeaponMainPullTrigger();
			}
			else if (_playerInput.Weapons.MainFire.WasReleasedThisFrame())
			{
				_characterFirearmControls.ReleaseWeaponMainPullTrigger();
			}
			
			if (_playerInput.Weapons.AlterFire.WasPressedThisFrame())
			{
				_characterFirearmControls.PressWeaponAlterPullTrigger();
			}
			else if (_playerInput.Weapons.AlterFire.WasReleasedThisFrame())
			{
				_characterFirearmControls.ReleaseWeaponAlterPullTrigger();
			}

			if (_playerInput.Weapons.ThrowAwayWeapon.WasPressedThisFrame())
			{
				_characterFirearmControls.ThrowAwayCurrentWeapon();
			}
			
			if (_playerInput.Weapons.Reload.WasPressedThisFrame())
			{
				_characterFirearmControls.ReloadCurrentWeapon();
			}

			float scroll = _playerInput.Weapons.ScrollInventory.ReadValue<float>();
			
			if (scroll >= 1)
			{
				_characterFirearmControls.ScrollWeapons(1);
			}
			else if (scroll <= -1)
			{
				_characterFirearmControls.ScrollWeapons(-1);
			}
		}
	}
}