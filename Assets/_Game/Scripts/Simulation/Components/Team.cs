using System;
using System.Collections.Generic;
using Scripts.Core.Model.Base;
using Scripts.Serialization.Containers;
using Scripts.Simulation.Model;
using Scripts.Simulation.Units.Model;
using Scripts.UI.Model;
using UnityEngine;

namespace Scripts.Simulation.Components
{
	public class Team <TUnit> : TeamBase, ISerializableContainer<TeamSerializationContainer> where TUnit : UnitModel, new()
	{
		public List<TUnit> Units { get; } = new List<TUnit>();
		public Color TeamColor { get; }
		
		private int _nextUnitInx;
		public Team(Color color, int inx)
		{
			TeamColor = color;
			ID = inx;
		}
		
		public bool TryAddUnit(TUnit unit)
		{
			unit.SetColor(TeamColor);
			if (Units.Contains(unit))
				return false;

			Units.Add(unit);
			return true;
		}
		
		public void RemoveUnit(UnitModel unit) => RemoveUnit(unit as TUnit);
		

		private void RemoveUnit(TUnit unit)
		{
			if(!Units.Contains(unit))
				return;
			
			if(Units.Count == 0)
				return;
			
			Units.Remove(unit);
			if(Units.Count == 0)
				TeamLose();
		}

		public TUnit GetNextUnit() => _nextUnitInx < Units.Count ? Units[_nextUnitInx++] : null;

		public override void SetDefault()
		{
			_nextUnitInx = 0;
			
			foreach (var unit in Units)
				unit.Restart();
			
			Units.Clear();
			base.SetDefault();
		}

		public override void Restart()
		{
			SetDefault();
			base.Restart();
		}

		public TeamSerializationContainer Serialize()
		{
			var units = new List<UnitSerializationContainer>();
			foreach (var unit in Units)
			{
				var serialized = unit.Serialize();
				if(serialized == null)
					continue;
				units.Add(serialized);
			}
					
			return new TeamSerializationContainer(ColorUtility.ToHtmlStringRGB(TeamColor), units);
		}

		public void Deserialize(TeamSerializationContainer container)
		{
		}
	}
}