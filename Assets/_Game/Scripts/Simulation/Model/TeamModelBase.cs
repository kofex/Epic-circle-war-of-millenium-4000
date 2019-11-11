using System;
using Scripts.Core.Interfaces;
using Scripts.Core.Model.Base;

namespace Scripts.Simulation.Model
{
	public class TeamModelBase : ModelBase, IUpdatable, IRestartable
	{
		public static event Action UpdateEnd;

		public virtual void Update(float dt)
		{
			UpdateEnd?.Invoke();
		}

		public virtual void Restart()
		{
		}
	}
}