using System;
using System.Collections.Generic;
using Scripts.Simulation.Units.Model;
using UnityEngine;

namespace Scripts.Simulation.Components
{
	public class Team <TUnit> where TUnit : UnitModel
	{
		public static event Action<int> Lose;

		public int ID { get; }
		public List<TUnit> Units { get; } = new List<TUnit>();
		public Color TeamColor { get; }
		
		public Team(Color color, int inx)
		{
			TeamColor = color;
			ID = inx;
			UnitModel.UnitDeath += (unit)=> RemoveUnit(unit as TUnit);
		}
		
		public bool TryAddUnit(TUnit unit)
		{
			unit.SetColor(TeamColor);
			if (Units.Contains(unit))
				return false;

			Units.Add(unit);
			return true;
		}

		public void RemoveUnit(TUnit unit)
		{
			if(Units.Contains(unit))
				Units.Remove(unit);
			if(Units.Count == 0)
				Lose?.Invoke(ID);
		}

	}
}