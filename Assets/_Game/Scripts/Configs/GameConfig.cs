using System;
using UnityEngine.Serialization;

namespace Scripts.Configs
{
	[Serializable]
	public class GameConfigs
	{
		public GameConfig GameConfig;
	}

	[Serializable]
	public class GameConfig
	{
		public int gameAreaWidth;
		public int gameAreaHeight;
		public int unitSpawnDelay;
		public int numUnitsToSpawn;
		public float minUnitRadius;
		public float maxUnitRadius;
		public float minUnitSpeed;
		public float maxUnitSpeed;
	}

}
