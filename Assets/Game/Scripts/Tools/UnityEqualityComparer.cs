using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
	public class UnityEqualityComparer<T> : IEqualityComparer<T> where T : Object
	{
		public bool Equals(T x, T y)
		{
			return ReferenceEquals(x, y) || x == y;
		}

		public int GetHashCode(T obj)
		{
			return obj.GetHashCode();
		}
	}
}