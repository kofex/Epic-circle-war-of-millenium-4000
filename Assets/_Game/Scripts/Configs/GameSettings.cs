using System.Linq;
using Scripts.Core.View;
using UnityEngine;

namespace Scripts.Configs
{
	[CreateAssetMenu(fileName = "GameSettings", menuName = "CircleWar/Create GameSettings")]
	public class GameSettings : ScriptableObject
	{
		public Color[] TeamColors;
		public string ExtConfigName = "Configs.txt";
		public CacheBehaviour[] Prefabs;
		public Vector2 BorderOffset;
		public Vector2 BorderBounceRandomOffset;
		public float MaxSpeedScale = 100.0f;
		public float DeathRadius = 0.2f;
		public float UiOffset = 4.0f;
		[Range(0f,1f)]
		public float AdditionShrinkDisplacement = 0.05f;

		public T GetPefab<T>() where T : CacheBehaviour
		{
			var t = Prefabs[0].GetType();
			var tt = typeof(T);
			var a = t == tt;
			var ans = Prefabs.FirstOrDefault(pref => pref.GetType() == typeof(T)) as T;
			return ans;
		}

	}
}