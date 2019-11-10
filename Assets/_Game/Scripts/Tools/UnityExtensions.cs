using UnityEngine;

namespace Scripts.Tools
{
	public static class UnityExtensions
	{
		public static Vector2 RandomNormalized(this Vector2 vector)
		{
			vector.x = UnityEngine.Random.Range(-1f, 1f);
			vector.y = UnityEngine.Random.Range(-1f, 1f);
			return vector;
		}
	}
}