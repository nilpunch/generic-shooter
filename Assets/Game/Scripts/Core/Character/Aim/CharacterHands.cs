﻿using UnityEngine;

namespace SM.FPS
{
	public class CharacterHands : MonoBehaviour
	{
		[SerializeField] private Transform _handlePivot;

		public Transform WeaponHandlePivot => _handlePivot;
	}
}