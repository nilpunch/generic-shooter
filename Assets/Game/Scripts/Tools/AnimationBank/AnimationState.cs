using System;
using UnityEngine;

namespace Tools
{
	public class AnimationState<T>: StateMachineBehaviour where T: Enum
	{
		[SerializeField] private T _state;

		public T State => _state;

		public event Action<T> EventStateEnter;
		public event Action<T> EventStateExit;

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.OnStateEnter(animator, stateInfo, layerIndex);
			EventStateEnter?.Invoke(_state);
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.OnStateExit(animator, stateInfo, layerIndex);
			EventStateExit?.Invoke(_state);
		}
	}
}