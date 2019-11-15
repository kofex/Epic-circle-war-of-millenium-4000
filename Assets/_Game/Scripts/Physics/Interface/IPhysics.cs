using UnityEngine;

namespace Scripts.Physics.Interface
{
	public interface IPhysics
	{
		void OnCollision(Vector2 collisionReflection);
	}
}