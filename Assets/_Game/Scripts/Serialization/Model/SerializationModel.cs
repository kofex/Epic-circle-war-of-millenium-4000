using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Scripts.Core;
using Scripts.Core.Model.Base;
using Scripts.Serialization.Containers;
using Scripts.Simulation.Model;
using Scripts.UI.Model;
using UnityEngine;

namespace Scripts.Serialization.Model
{
	public class SerializationModel : SerializationModelBase
	{
		private const string SIMULATION_KEY = "sim_key";
		
		public new SerializationModel InitModel()
		{
			MainUIModel.SaveBtnClick += Serialize;
			MainUIModel.LoadBtnClick += Deserialize;
			return this;
		}
		
		protected override void Serialize()
		{
			var simulation = GameCore.GetModel<SimulationModel>().Serialize();
			if(simulation == null)
				return;

			PlayerPrefs.SetString(SIMULATION_KEY, BinarySerialize(simulation));
		}

		
		protected override void Deserialize()
		{
			var prefData = Convert.FromBase64String(PlayerPrefs.GetString(SIMULATION_KEY));

			if (prefData.Length == 0)
			{
				Debug.Log($"No data in prefs", LogType.Warning);
				return;
			}

			var simulation = BinaryDeserialization<SimulationSerializationContainer>(prefData);
			GameCore.GetModel<SimulationModel>().Deserialize(simulation);
		}
	}
}