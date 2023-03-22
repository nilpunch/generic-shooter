namespace SM.FPS
{
	public interface IWeaponMagazine
	{
		void Reload();
		void ChargeAmmo(int amount);
	}
}