using Scripts.Components;
using Scripts.Core.Interfaces;
using Scripts.Core.Model.Base;
using Scripts.Simulation.Model;

namespace Scripts.Core.Model
{
	public class MainModel : ModelBase, IUpdatable
	{
		public SingletonModelsContainer SingletonModels { get; } = new SingletonModelsContainer();

		private IUpdatable[] _updatableModels; 

		public new MainModel InitModel()
		{
			SingletonModels.TryAddSingletonModel(CreateModel<SettingsModel>()).InitModel();
			SingletonModels.TryAddSingletonModel(CreateModel<SimulationModel>()).InitModel();

			_updatableModels = SingletonModels.GetUpdatableModels();
				
			return this;
		}

		public void Update(float dt)
		{
			foreach (var model in _updatableModels)
			{
				model.Update(dt);
			}
		}
	}
}