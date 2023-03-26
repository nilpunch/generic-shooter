namespace SM.FPS
{
	/// <summary>
	/// Controls for some weapon action.
	/// </summary>
	public interface ITrigger
	{
		void Press();
		void Release();
	}
}