using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
	[Serializable]
	public struct AnimationStateInfo<TAnimationType> where TAnimationType: Enum
	{
		[field: SerializeField] public TAnimationType State { get; set; }
		[field: SerializeField] public int NameHash { get; set; }
		[field: SerializeField] public int LayerIndex { get; set; }
	}
	
	public abstract class AnimationsBank<TAnimationType, TAnimationStateType>: MonoBehaviour 
		where TAnimationType: Enum
		where TAnimationStateType: AnimationState<TAnimationType>
	{
		[SerializeField] private Animator _animator;
		[Space, SerializeField] private AnimationStateInfo<TAnimationType>[] _database;

		public bool TryGetAnimation(TAnimationType state, out int nameHash, out int layerIndex)
		{
			foreach (AnimationStateInfo<TAnimationType> info in _database)
			{
				if (info.State.Equals(state))
				{
					nameHash = info.NameHash;
					layerIndex = info.LayerIndex;
					return true;
				}
			}

			nameHash = default;
			layerIndex = default;
			return false;
		}

#if UNITY_EDITOR
		[ContextMenu("Bake Info")]
		protected virtual void BakeInfo()
		{
			Dictionary<TAnimationType, KeyValuePair<int, int>> database =
				new Dictionary<TAnimationType, KeyValuePair<int, int>>();
			if (_animator.runtimeAnimatorController is UnityEditor.Animations.AnimatorController animatorController)
			{
				for (int index = 0; index < animatorController.layers.Length; index++)
				{
					var layer = animatorController.layers[index];
					foreach (var childAnimatorState in layer.stateMachine.states)
					foreach (var behaviour in childAnimatorState.state.behaviours)
					{
						if (behaviour is TAnimationStateType stateBehaviour &&
							!database.ContainsKey(stateBehaviour.State))
						{
							database.Add(
								stateBehaviour.State,
								new KeyValuePair<int, int>(
									childAnimatorState.state.nameHash,
									index));
						}
					}
				}
			}

			_database = new AnimationStateInfo<TAnimationType>[database.Count];
			int i = -1;
			foreach (var info in database)
			{
				i++;
				_database[i] = new AnimationStateInfo<TAnimationType>()
				{
					State = info.Key,
					NameHash = info.Value.Key,
					LayerIndex = info.Value.Value,
				};
			}
			
			UnityEditor.EditorUtility.SetDirty(this);
		}
#endif
	}
}