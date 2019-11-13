using System;
using System.Collections.Generic;
using Scripts.Components;
using Scripts.Configs;
using Scripts.Core;
using Scripts.Core.Interfaces;
using Scripts.Core.Model;
using Scripts.Core.Model.Base;
using Scripts.Physics.Model;
using Scripts.Serialization.Containers;
using Scripts.Simulation.Camera.Model;
using Scripts.Simulation.Components;
using Scripts.Simulation.Units.Model;
using Scripts.UI.Model;
using UnityEngine;

namespace Scripts.Simulation.Model
{
	public class SimulationModel : ModelBase, IUpdatable, IRestartable, ISerializableContainer<SimulationSerializationContainer>
	{
		public static Action SimulationRestartBegin;
		public static Action<float, Color> SimulationEnd;
		public static bool IsGameOver => _isGameOver;
		private static bool _isGameOver;
		
		public SingletonModelsContainer SimulationSingletons { get; } = new SingletonModelsContainer();
		

		private GameConfig _gameConfig;
		private CirclePhysics _physicsModel;
		private AreaModel _areaModel;
		private TeamsModel<CircleUnitModel> _teamsModel;
		private IUpdatable[] _updatableModels;
		private bool _isSpawned;
		private float _timer;
		private float _simulationTime;
		
		
		public new SimulationModel InitModel()
		{
			_gameConfig = GameCore.GetModel<SettingsModel>().GameConfigs.GameConfig;
			_physicsModel = SimulationSingletons.TryAddSingletonModel(CreateModel<CirclePhysics>()).InitModel();
			_areaModel = SimulationSingletons.TryAddSingletonModel(CreateModel<AreaModel>())
				.InitModel()
				.SetView(null).Model;
			TeamsModelBase.UnitsSpawned += OnSpawnComplete;

			_teamsModel = SimulationSingletons.TryAddSingletonModel(CreateModel<TeamsModel<CircleUnitModel>>())
				.InitModel();
			SimulationSingletons.TryAddSingletonModel(CreateModel<CameraModel>())
				.InitModel(_gameConfig.gameAreaWidth, _gameConfig.gameAreaHeight)
				.SetView();

			_updatableModels = SimulationSingletons.GetUpdatableModels();
			
			TeamBase.Lose += inx =>
			{
				_isGameOver = true;
				SimulationEnd?.Invoke(_simulationTime, _teamsModel.TheVictoriousTeam.TeamColor);
			};

			MainUIModel.NewBtnClick += Restart;
			
			return this;
		}

		public void Update(float dt)
		{
			if (_isGameOver)
				return;
			
			if (!_isSpawned)
			{
				SpawnUnitOnField(dt);
				return;
			}
			
			_physicsModel.UpdatePhys(dt);
			
			foreach (var model in _updatableModels)
			{
				model.Update((dt));
			}

			_simulationTime += dt;
		}


		private void SpawnUnitOnField(float dt)
		{
			_timer += dt;
			if (_timer < _gameConfig.unitSpawnDelay / 1000)
				return;
			
			_areaModel.SpawnUnit(_teamsModel.GetNextUnit());
			_timer = 0;
		}

		private void OnSpawnComplete() => _isSpawned = true;

		public void Restart()
		{
			SimulationRestartBegin?.Invoke();

			SetDefault();

			foreach (var model in SimulationSingletons.GetRestartableModels())
				model.Restart();
		}
		
		public void SetDefault()
		{
			UnitModel.RestId();
			
			_isSpawned = false;
			_isGameOver = false;
			_simulationTime = 0f;
		}

		public SimulationSerializationContainer Serialize()
		{
			if (!_isSpawned)
				return null;
				
			var teamsList = new List<TeamSerializationContainer>();
			foreach (var team in _teamsModel.Teams)
				teamsList.Add(team.Serialize());

			return new SimulationSerializationContainer(teamsList, _simulationTime);
		}

		public void Deserialize(SimulationSerializationContainer container)
		{
			if(!_isSpawned)
				return;
			
			SetDefault();
			foreach (var model in SimulationSingletons.GetRestartableModels())
				model.SetDefault();
			
			_simulationTime = container.SimulationTime;
			_teamsModel.SetSerializedTeams(container.Teams);

			var count = _teamsModel.Teams[0].Units.Count + _teamsModel.Teams[1].Units.Count;
			_teamsModel.SetMaxUnits(count);
			
			for(var i = 0; i < _teamsModel.MaxUnits; i++)
			{
				var unit = _teamsModel.GetNextUnit();
				_areaModel.SpawnUnit(unit, false);
			} 
		}
	}
}