using UnityEngine;

namespace SM.FPS
{
	public interface IPhysics
	{
		public Vector3 Position { get; }
		
		/// <summary>
		/// Object velocity without inherited forces.
		/// </summary>
		public Vector3 Velocity { get; }

		/// <summary>
		/// Depenetration from physics engine, applied to correct velocity.
		/// </summary>
		public Vector3 Depenetration { get; }
		
		/// <summary>
		/// Add velocity, that just affect final calculations. Does not affect <see cref="Velocity"/>.
		/// </summary>
		/// <param name="velocity">Force as velocity change.</param>
		public void AddInheritedForce(Vector3 velocity);
		
		/// <summary>
		/// Add velocity change. 
		/// </summary>
		/// <param name="velocity">Force as velocity change.</param>
		public void AddForce(Vector3 velocity);
	}
}