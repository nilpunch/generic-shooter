using UnityEngine;

namespace SM.FPS
{
	public class WeaponAimedAttack : MonoBehaviour, IWeaponAim
	{
		[SerializeField] private WeaponAttack _weaponAttack;

		private Vector3 _position;
		private Vector3 _direction;

		public void SetShootPositionAndDirection(Vector3 position, Vector3 direction)
		{
			_position = position;
			_direction = direction;
		}

		/// <summary>
		/// Method for performing attack.
		/// </summary>
		/// <remark>
		/// This method needed for abstracting out aim routine. So, user care only about preforming attack, and not aiming.
		/// </remark>
		public void AimedAttack()
		{
			_weaponAttack.Attack(_position, _direction);
		}
	}
}