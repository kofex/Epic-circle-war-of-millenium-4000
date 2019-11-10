using Scripts.Core.Model.Base;
using UnityEngine;

namespace Scripts.Physics.Model
{
	public class PhysicsBase : ModelBase
	{
		public static Vector2 GetDistance(Vector2 from, Vector2 to)
		{
			return new Vector2(from.x - to.x, from.y - to.y);
		}
	}
}