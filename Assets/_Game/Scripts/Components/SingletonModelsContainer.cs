using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Core.Interfaces;
using Scripts.Core.Model.Base;
using Scripts.Simulation;

namespace Scripts.Components
{
	public class SingletonModelsContainer
	{
		private Dictionary<Type, ModelBase> SingletonModels { get; } = new Dictionary<Type, ModelBase>();

		public T TryAddSingletonModel<T>(T model) where T : ModelBase
		{
			if (model == null)
			{
				Debug.Log($"Passing model is null!", LogType.Error);
				return null;
			}

			if (TryGetSingletonModel<T>() == null)
			{
				SingletonModels.Add(typeof(T), model);
				return model;
			}

			Debug.Log($"Model is already exists!", LogType.Warning);
			return model;
		}

		public T TryGetSingletonModel<T>() where T : ModelBase
		{
			if (SingletonModels == null)
			{
				Debug.Log($"Singleton dictionary is null!", LogType.Error);
				return null;
			}

			var type = typeof(T);

			if (SingletonModels.ContainsKey(type))
				return SingletonModels[type] as T;
			
			return null;
		}

		public IUpdatable[] GetUpdatableModels() => SingletonModels.Where(pairs => pairs.Value is IUpdatable)
			.Select(pairs => pairs.Value as IUpdatable).ToArray().ToArray();
		
		public IRestartable[] GetRestartableModels() => SingletonModels.Where(pairs => pairs.Value is IRestartable)
			.Select(pairs => pairs.Value as IRestartable).ToArray().ToArray();
	}
}