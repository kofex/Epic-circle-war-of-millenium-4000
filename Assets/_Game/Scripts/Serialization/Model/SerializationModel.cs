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
	public class SerializationModel : ModelBase
	{
		private const string SIMULATION_KEY = "sim_key";
		
		public new SerializationModel InitModel()
		{
			MainUIModel.SaveBtnClick += Serialize;
			MainUIModel.LoadBtnClick += Deserialize;
			return this;
		}
		
		private void Serialize()
		{
			var simulation = GameCore.GetModel<SimulationModel>().Serialize();
			if(simulation == null)
				return;
			
			var formatter = new BinaryFormatter();
			
			using (var stream = new MemoryStream())
			{
				var serializer = new BinaryFormatter();
				serializer.Serialize(stream, simulation);
				var str = Convert.ToBase64String(stream.ToArray());
				PlayerPrefs.SetString(SIMULATION_KEY, str);
			}
		}

		
		private void Deserialize()
		{
			var b = Convert.FromBase64String(PlayerPrefs.GetString(SIMULATION_KEY));

			if (b.Length == 0)
			{
				Debug.Log($"No data in prefs", LogType.Warning);
				return;
			}
			
			SimulationSerializationContainer simulation;
			using (var stream = new MemoryStream(b))
			{
				var deserializer = new BinaryFormatter();
				simulation =  deserializer.Deserialize(stream) as SimulationSerializationContainer;
			}
			
			GameCore.GetModel<SimulationModel>().Deserialize(simulation);
			
			
		}
	}
}