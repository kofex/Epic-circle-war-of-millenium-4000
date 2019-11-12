using System.Collections.Generic;
using Scripts.Core.Model.Base;
using Scripts.Simulation;
using Scripts.Simulation.Model;
using Scripts.Simulation.Units.Model;
using UnityEngine;

namespace Scripts.Physics.Model
{
	public class PhysicsModel<TUnit> : PhysicsBase, IRestartable where TUnit : UnitModel, new()
	{
		protected BorderRect BorderRect;
		protected List<TUnit> Units = new List<TUnit>();

		protected new PhysicsModel<TUnit> InitModel()
		{
			AreaModel.BordersCreated += RegisterBorder;
			TeamsModel<TUnit>.UnitAdded += RegisterUnit;
			UnitModel.UnitDeath += (unit)=>RemoveUnit(unit as TUnit);
			
			return this;
		}

		private void RegisterBorder(BorderRect rect)
		{
			BorderRect = rect;
		}

		private void RegisterUnit(TUnit unit)
		{
			Units.Add(unit);
		}


		public virtual void UpdatePhys(float dt)
		{
			CheckBorderCollision();
			CheckUnitsCollision();
		}
		
		protected virtual void CheckBorderCollision()
		{
		}

		protected virtual void CheckUnitsCollision()
		{
		}
		
		protected void RemoveUnit(TUnit unit)
		{
			if(Units.Contains(unit))
				Units.Remove(unit);
		}

		public void SetDefault()
		{
			Units.Clear();
		}

		public void Restart()
		{
			SetDefault();
		}
	}
}