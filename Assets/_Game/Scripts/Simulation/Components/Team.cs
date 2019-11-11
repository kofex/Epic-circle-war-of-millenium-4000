using System;
using System.Collections.Generic;
using Scripts.Simulation.Model;
using Scripts.Simulation.Units.Model;
using UnityEngine;

namespace Scripts.Simulation.Components
{
	public class Team <TUnit> : TeamBase where TUnit : UnitModel
	{
		public List<TUnit> Units { get; } = new List<TUnit>();
		public Color TeamColor { get; }
		
		public Team(Color color, int inx)
		{
			TeamColor = color;
			ID = inx;
			UnitModel.UnitDeath += RemoveUnit;
		}
		
		public bool TryAddUnit(TUnit unit)
		{
			unit.SetColor(TeamColor);
			if (Units.Contains(unit))
				return false;

			Units.Add(unit);
			return true;
		}
		
		private void RemoveUnit(UnitModel unit) => RemoveUnit(unit as TUnit);
		

		private void RemoveUnit(TUnit unit)
		{
			if(Units.Contains(unit))
				Units.Remove(unit);
			if(Units.Count == 0)
				TeamLose();
		}

		public override void Restart()
		{
			UnitModel.UnitDeath -= RemoveUnit;
			
			foreach (var unit in Units)
				unit.Restart();
			
			Units.Clear();
					
			base.Restart();
		}
	}
}