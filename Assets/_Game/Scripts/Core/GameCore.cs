using Scripts.Core.Model;
using Scripts.Core.Model.Base;
using Scripts.Simulation.Model;
using UnityEngine;

namespace Scripts.Core
{
	public class GameCore : MonoBehaviour
	{		
		private static GameCore Instance { get; set; }
		private MainModel _mainModel;
		[SerializeField] [Range(0.0f, 100.0f)] private float _gameSpeed = 1.0f;
		
		private void Awake()
		{
			if (Instance)
				DestroyImmediate(this);
			
			Instance = this;
			_mainModel = new MainModel();
			_mainModel.InitModel();
			
			DontDestroyOnLoad(this);
		}

		private void Update()
		{
			_mainModel.Update(Time.deltaTime * _gameSpeed);
		}

		public static T GetModel<T>() where T : ModelBase
		{
			return Instance._mainModel.SingletonModels.TryGetSingletonModel<T>();
		}
		
		public static T GetModelFromSimulations<T>() where T : ModelBase
		{
			return GetModel<SimulationModel>().SimulationSingletons.TryGetSingletonModel<T>();
		}
	}
}