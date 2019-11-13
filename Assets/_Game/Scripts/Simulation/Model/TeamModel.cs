using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Core;
using Scripts.Core.Model;
using Scripts.Serialization.Containers;
using Scripts.Simulation.Components;
using Scripts.Simulation.Units.Model;
using Scripts.UI.Model;
using UnityEngine;

namespace Scripts.Simulation.Model
{
	public class TeamsModel<TUnit> : TeamsModelBase where TUnit : UnitModel, new()
	{
		public Team<TUnit> TheVictoriousTeam;
		public static event Action<TUnit> UnitAdded;
		public static event Action<int, int> UnitCountChange;
		public List<Team<TUnit>> Teams { get; } = new List<Team<TUnit>>();
		public int MaxUnits { get; private set; }
		private int _nextTeamInx;
		private int _unitsGot;
		private bool _canMove;
		private SettingsModel _gameResources;

		public new TeamsModel<TUnit> InitModel()
		{
			_gameResources = GameCore.GetModel<SettingsModel>();
			MaxUnits = _gameResources.GameConfigs.GameConfig.numUnitsToSpawn;

			CreateTeams();
			PrepareCircleUnits();
			
			TeamBase.Lose += OnTeamLose;
			UnitModel.UnitDeath += (unit) => UnitCountChange?.Invoke(Teams[0].Units.Count, Teams[1].Units.Count);
			return this;
		}

		private void CreateTeams()
		{
			var colors = _gameResources.GameSettings.TeamColors;
			var inx = 0;
			Teams.Add(new Team<TUnit>(colors[0], inx++));
			Teams.Add(new Team<TUnit>(colors[1], inx++));
		}

		//TODO: решить вопрос с CreateModel<CircleUnitModel>() (нужно так: CreateModel<TUnit>() )
		private void PrepareCircleUnits()
		{
			for (var i = 0; i < MaxUnits; i++)
			{
				var unitModel = CreateModel<CircleUnitModel>().InitModel(_gameResources);
				TryAddUnit(i, unitModel);
			}
		}

		private void TryAddUnit(int totalUnitInx, CircleUnitModel unitModel)
		{
			if (!Teams[GetTeamIndex(totalUnitInx)].TryAddUnit(unitModel as TUnit))
			{
				Debug.Log($"{unitModel.UnitId} is already exists", LogType.Warning);
				return;
			}

			UnitAdded?.Invoke(unitModel as TUnit);
		}

		public UnitModel GetNextUnit()
		{
			if (_unitsGot++ >= MaxUnits)
				OnSpawnCompleted();
			
			return Teams[GetTeamIndex(_nextTeamInx++)].GetNextUnit();;
		}

		private int GetTeamIndex(int totalUnitInx) => totalUnitInx % 2 == 0 ? 0 : 1;

		public override void Update(float dt)
		{
			if(!_canMove)
				return;
			
			foreach (var team in Teams)
				team.Units.ForEach(un => un.Update(dt));
			
			base.Update(dt);
		}

		protected override void OnSpawnCompleted()
		{
			base.OnSpawnCompleted();
			_canMove = true;
		}

		private void OnTeamLose(int id)
		{
			TheVictoriousTeam = Teams.FirstOrDefault(team => !team.HasLoose);
			Debug.Log($"Victory to {TheVictoriousTeam?.ID}");
		}

		public override void SetDefault()
		{
			foreach (var team in Teams)
				team.SetDefault();
			
			Teams.Clear();

			_canMove = false;
			_nextTeamInx = 0;
			_unitsGot = 0;
			MaxUnits = _gameResources.GameConfigs.GameConfig.numUnitsToSpawn;
		}

		public void SetMaxUnits(int max) => MaxUnits = max;

		public override void Restart()
		{
			SetDefault();
			CreateTeams();
			PrepareCircleUnits();
		}

		public void SetSerializedTeams(List<TeamSerializationContainer> teams)
		{
			var inx = 0;
			foreach (var team in teams)
			{
				var col = $"#{team.TeamColor}";
				ColorUtility.TryParseHtmlString(col, out var color);
				Teams.Add(new Team<TUnit>(color, inx++));
			}

			var maxUnits = teams[0].Units.Count + teams[1].Units.Count;
			var unitInx = 0;
			for (var i = 0; i < maxUnits; i++)
			{
				var teamInx = GetTeamIndex(i);
				
				var deserializedUnit = CreateModel<CircleUnitModel>();
				deserializedUnit.Deserialize(teams[teamInx].Units[unitInx]);
				if (teamInx == 1)
					unitInx++;
				
				TryAddUnit(i, deserializedUnit);
			}
		}
	}
}