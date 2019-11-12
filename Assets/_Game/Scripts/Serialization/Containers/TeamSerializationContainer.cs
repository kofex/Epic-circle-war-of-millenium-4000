using System;
using System.Collections.Generic;
using Scripts.UI.Model;
using UnityEngine;

namespace Scripts.Serialization.Containers
{
	[Serializable]
	public class TeamSerializationContainer : IMySerializable
	{
		public string TeamColor;
		public List<UnitSerializationContainer> Units;
		
		public TeamSerializationContainer()
		{
			
		}

		public TeamSerializationContainer(string teamColor, List<UnitSerializationContainer> units)
		{
			TeamColor = teamColor;
			Units = units;
		}
		
	}
}