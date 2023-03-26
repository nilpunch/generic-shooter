using UnityEngine;

namespace SM.FPS
{
	public interface IWeaponAim
	{
		void SetShootPositionAndDirection(Vector3 position, Vector3 direction);
	}
}