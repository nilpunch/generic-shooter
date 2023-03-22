using UnityEngine;

namespace SM.FPS
{
	public class CharacterWeapon : MonoBehaviour
	{
		[field: SerializeField] public WeaponComponents WeaponComponents { get; private set; }
		[field: SerializeField] public HandledWeapon HandledWeapon { get; private set; }
		[field: SerializeField] public WeaponAimedAttack Aim { get; private set; }
	}
}