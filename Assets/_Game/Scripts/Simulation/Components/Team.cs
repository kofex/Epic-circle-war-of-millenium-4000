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

		public override void SetDefault()
		{
			UnitModel.UnitDeath -= RemoveUnit;
			
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
				units.Add(unit.Serialize());
					
			return new TeamSerializationContainer(ColorUtility.ToHtmlStringRGB(TeamColor), units);
		}

		public void Deserialize(TeamSerializationContainer container)
		{
		}

		public void Deserialize(TeamSerializationContainer container, Action<TUnit> onUnitAdded)
		{
			foreach (var unit in container.Units)
			{
				var deserializedUnit = ModelBase.CreateModel<TUnit>();
				deserializedUnit.Deserialize(unit);
				TryAddUnit(deserializedUnit);
				onUnitAdded?.Invoke(deserializedUnit);
			} 
		}
	}
}