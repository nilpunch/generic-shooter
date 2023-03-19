using UnityEngine;

namespace SM.FPS
{
	public class WeaponAimedAttack : MonoBehaviour
	{
		[SerializeField] private WeaponAttack _weaponAttack;
		
		private Aim _aim;

		public void SetAim(Aim aim)
		{
			_aim = aim;
		}

		public void AimedAttack()
		{
			_weaponAttack.Attack(_aim.WorldPosition, _aim.WorldDirection);
		}
	}
}