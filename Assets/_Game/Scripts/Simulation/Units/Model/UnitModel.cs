using System;
using Scripts.Core.Interfaces;
using Scripts.Core.Model;
using Scripts.Physics.Interface;
using Scripts.Simulation.Model;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.Simulation.Units.Model
{
	public class UnitModel : UnitModelBase, IModelWithVIew<UnitView>, IUpdatable, IPhysics, IRestartable
	{
		public static event Action<UnitModel> UnitDeath;
		protected static int ID;
		
		public UnitView View => ThisView;
		protected Color Color { get; private set; }
		public void SetColor(Color color) => Color = color;
		
		protected UnitView ThisView;

		protected UnitModel InitModel(SettingsModel settings)
		{
			SimulationModel.SimulationRestartBegin += () => ID = 0;
			InitModel();
			return this;
		}

		public virtual UnitView SetView(Transform parent = null) => View;

		public virtual void Update(float dt)
		{
			View.transform.Translate(CurrentSpeed * dt);
		}

		public virtual void OnCollision(Vector2 collisionReflection, object other = null)
		{
		}
		
		protected virtual void OnDeath()
		{
			UnitDeath?.Invoke(this);
			Object.Destroy(View.gameObject);
		}

		public virtual void Restart()
		{
			Object.Destroy(View.gameObject);
		}
	}
}