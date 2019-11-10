using System;
using Scripts.Core.Interfaces;
using Scripts.Core.Model.Base;

namespace Scripts.Simulation.Model
{
	public class TeamModelBase : ModelBase, IUpdatable
	{
		public static event Action UpdateEnd;

		public virtual void Update(float dt)
		{
			UpdateEnd?.Invoke();
		}
	}
}