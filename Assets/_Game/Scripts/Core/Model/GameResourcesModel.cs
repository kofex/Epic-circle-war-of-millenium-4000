using System;
using System.IO;
using Scripts.Configs;
using Scripts.Core.Model.Base;
using UnityEngine;


namespace Scripts.Core.Model
{
	public class SettingsModel : ModelBase
	{
		private string PathToConfigs => Application.streamingAssetsPath;
		
		public GameConfigs GameConfigs { get; private set; }
		public GameSettings GameSettings{ get; private set; }

		public new SettingsModel InitModel()
		{
			LoadSettings();
			LoadConfig();
			return this;
		}

		private void LoadSettings()
		{
			GameSettings = Resources.Load<GameSettings>(typeof(GameSettings).Name);
		}

		private void LoadConfig()
		{
			var path = Path.Combine(PathToConfigs, GameSettings?.ExtConfigName ?? throw new Exception("There is no GameSettings"));

			if (!File.Exists(path))
			{
				Debug.Log($"Config file is missing!", LogType.Error);
				return;
			}

			var dataStr = File.ReadAllText(path);
			GameConfigs = JsonUtility.FromJson<GameConfigs>(dataStr);
		}
		
	}
}