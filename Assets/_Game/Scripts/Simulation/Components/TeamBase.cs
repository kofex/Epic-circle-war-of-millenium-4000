using System;
using Scripts.UI.Model;
using UnityEngine;

namespace Scripts.Simulation.Components
{
	public class TeamBase : IRestartable
	{
		public static event Action<int> Lose;

		public int ID { get; protected set; }
		public bool HasLoose { get; protected set; }
		
		protected virtual void TeamLose()
		{
			HasLoose = true;
			Lose?.Invoke(ID);
		}

		public virtual void Restart()
		{
			HasLoose = false;
			ID = 0;
		}
	}
}