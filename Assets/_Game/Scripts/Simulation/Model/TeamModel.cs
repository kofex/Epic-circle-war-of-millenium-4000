using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Core;
using Scripts.Core.Model;
using Scripts.Simulation.Components;
using Scripts.Simulation.Units.Model;

namespace Scripts.Simulation.Model
{
	public class TeamModel<TUnit> : TeamModelBase where TUnit : UnitModel, new()
	{
		public Team<TUnit> TheVictoriousTeam;
		public static event Action<TUnit> UnitAdded; 
		public List<Team<TUnit>> Teams { get; } = new List<Team<TUnit>>();
		public int MaxUnits { get; private set; }
		private int _nextUnitInx;
		private int _nextTeamInx;
		private bool _canMove;

		private SettingsModel _gameResources;
		
		public new TeamModel<TUnit> InitModel()
		{
			_gameResources = GameCore.GetModel<SettingsModel>();
			MaxUnits = _gameResources.GameConfigs.GameConfig.numUnitsToSpawn;
			var colors = _gameResources.GameSettings.TeamColors;

			var inx = 0;
			Teams.Add(new Team<TUnit>(colors[0], inx++));
			Teams.Add(new Team<TUnit>(colors[1], inx++));

			PrepareCircleUnits();
			
			AreaModel.UnitsSpawned += OnSpawnCompleted;
			Team<TUnit>.Lose += OnTeamLose;
			return this;
		}

		//TODO: решить вопрос с T
		private void PrepareCircleUnits()
		{
			for (var i = 0; i < MaxUnits; i++)
			{
				var unitModel = CreateModel<CircleUnit>().InitModel(_gameResources);
				Teams[i % 2 == 0 ? 0 : 1].TryAddUnit(unitModel as TUnit);
				UnitAdded?.Invoke(unitModel as TUnit);
			}
		}

		public UnitModel GetNextUnit()
		{
			if (_nextUnitInx >= Teams[0].Units.Count || _nextUnitInx >= Teams[1].Units.Count)
				return null;
			return _nextTeamInx++ % 2 == 0 ? Teams[0].Units[_nextUnitInx] : Teams[1].Units[_nextUnitInx++];
		}

		public override void Update(float dt)
		{
			if(!_canMove)
				return;
			
			foreach (var team in Teams)
				team.Units.ForEach(un => un.Update(dt));
			
			base.Update(dt);
		}

		private void OnSpawnCompleted()
		{
			_canMove = true;
		}

		private void OnTeamLose(int id)
		{
			Teams.RemoveAt(id);
			TheVictoriousTeam = Teams.FirstOrDefault();
			Debug.Log($"Victory to {TheVictoriousTeam.ID}");
		}
	}
}