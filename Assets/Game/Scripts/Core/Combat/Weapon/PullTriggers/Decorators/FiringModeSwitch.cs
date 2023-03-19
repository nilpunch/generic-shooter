using UnityEngine;

namespace SM.FPS
{
	public class FiringModeSwitch : TriggerPull, IFiringModeSwitch
	{
		[SerializeField] private TriggerPull[] _firingModes;

		private int _currentFiringMode;

		public override void Press()
		{
			_firingModes[_currentFiringMode].Press();
		}

		public override void Release()
		{
			_firingModes[_currentFiringMode].Release();
		}

		public void NextFiringMode()
		{
			_currentFiringMode = (_currentFiringMode + 1) % _firingModes.Length;
		}
	}
}