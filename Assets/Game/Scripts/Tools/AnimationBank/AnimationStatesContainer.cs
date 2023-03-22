using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
	public class AnimationStatesContainer<T>: MonoBehaviour where T : Enum
	{
		[SerializeField] private Animator _animator;
        
		private readonly HashSet<T> _currentStates = new();

		private void Awake()
		{
			AnimationState<T>[] stateBehaviours = _animator.GetBehaviours<AnimationState<T>>();
			foreach (AnimationState<T> stateBehaviour in stateBehaviours)
			{
				stateBehaviour.EventStateEnter += OnStateEnter;
				stateBehaviour.EventStateExit += OnStateExit;
			}
		}

		public void OnDestroy()
		{
			AnimationState<T>[] stateBehaviours = _animator.GetBehaviours<AnimationState<T>>();
			foreach (AnimationState<T> stateBehaviour in stateBehaviours)
			{
				stateBehaviour.EventStateEnter -= OnStateEnter;
				stateBehaviour.EventStateExit -= OnStateExit;
			}
		}

		public bool ContainsState(T state)
		{
			return _currentStates.Contains(state);
		}

		private void OnStateEnter(T state)
		{
			if (!_currentStates.Contains(state))
			{
				_currentStates.Add(state);
			}
		}

		private void OnStateExit(T state)
		{
			_currentStates.Remove(state);
		}
	}
}