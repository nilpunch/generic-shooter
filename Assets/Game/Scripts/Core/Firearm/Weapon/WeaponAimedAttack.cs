using UnityEngine;

namespace SM.FPS
{
	public class WeaponAimedAttack : MonoBehaviour
	{
		[SerializeField] private WeaponAttack _weaponAttack;
		
		private CharacterAim _characterAim;

		public void Aim(CharacterAim characterAim)
		{
			_characterAim = characterAim;
		}

		public void AimedAttack()
		{
			_weaponAttack.Attack(_characterAim.WorldPosition, _characterAim.WorldDirection);
		}
	}
}