using System;
using Scripts.Components;
using Scripts.Configs;
using Scripts.Core;
using Scripts.Core.Interfaces;
using Scripts.Core.Model;
using Scripts.Core.Model.Base;
using Scripts.Physics.Model;
using Scripts.Simulation.Camera.Model;
using Scripts.Simulation.Components;
using Scripts.Simulation.Units.Model;
using UnityEngine;

namespace Scripts.Simulation.Model
{
	public class SimulationModel : ModelBase, IUpdatable, IRestartable
	{
		public static Action SimulationRestartBegin;
		public static Action<float, Color> SimulationEnd;
		public static bool IsGameOver => _isGameOver;
		private static bool _isGameOver;
		
		public SingletonModelsContainer SimulationSingletons { get; } = new SingletonModelsContainer();
		

		private GameConfig _gameConfig;
		private CirclePhysics _physicsModel;
		private AreaModel _areaModel;
		private TeamModel<CircleUnitModel> _teamModel;
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
			AreaModel.UnitsSpawned += OnSpawnComplete;

			_teamModel = SimulationSingletons.TryAddSingletonModel(CreateModel<TeamModel<CircleUnitModel>>())
				.InitModel();
			SimulationSingletons.TryAddSingletonModel(CreateModel<CameraModel>())
				.InitModel(_gameConfig.gameAreaWidth, _gameConfig.gameAreaHeight)
				.SetView();

			_updatableModels = SimulationSingletons.GetUpdatableModels();
			
			TeamBase.Lose += inx =>
			{
				_isGameOver = true;
				SimulationEnd?.Invoke(_simulationTime, _teamModel.TheVictoriousTeam.TeamColor);
			};
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
			
			_areaModel.SpawnUnit(_teamModel.GetNextUnit());
			_timer = 0;
		}

		private void OnSpawnComplete() => _isSpawned = true;


		public void Restart()
		{
			SimulationRestartBegin?.Invoke();
			
			_isSpawned = false;
			_isGameOver = false;
			_simulationTime = 0f;
			
			foreach (var model in SimulationSingletons.GetRestartableModels())
				model.Restart();
		}
	}
}