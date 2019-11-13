using System;
using Scripts.Core.Interfaces;
using Scripts.Core.Model.Base;

namespace Scripts.Simulation.Model
{
	public class TeamsModelBase : ModelBase, IUpdatable, IRestartable
	{
		public static event Action UpdateEnd, UnitsSpawned;

		public virtual void Update(float dt)
		{
			UpdateEnd?.Invoke();
		}
		
		protected virtual void OnSpawnCompleted()
		{
			UnitsSpawned?.Invoke();
		}

		public virtual void SetDefault()
		{
		}

		public virtual void Restart()
		{
		}
	}
}