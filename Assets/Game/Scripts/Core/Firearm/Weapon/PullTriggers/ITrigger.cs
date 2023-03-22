namespace SM.FPS
{
	/// <summary>
	/// Determines fire of a weapon.
	/// </summary>
	public interface ITrigger
	{
		void Press();
		void Release();
	}
}