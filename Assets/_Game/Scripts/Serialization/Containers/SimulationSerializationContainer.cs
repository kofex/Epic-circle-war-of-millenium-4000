using System;
using System.Collections.Generic;
using Scripts.UI.Model;

namespace Scripts.Serialization.Containers
{
	[Serializable]
	public class SimulationSerializationContainer : IMySerializable
	{
		public SimulationSerializationContainer()
		{
			
		}

		public SimulationSerializationContainer(List<TeamSerializationContainer> teams, float simulationTime)
		{
			Teams = teams;
			SimulationTime = simulationTime;
		}
		
		public List<TeamSerializationContainer> Teams;
		public float SimulationTime;
	}
}