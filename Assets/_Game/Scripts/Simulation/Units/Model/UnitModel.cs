using System;
using Scripts.Core.Interfaces;
using Scripts.Core.Model;
using Scripts.Physics.Interface;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.Simulation.Units.Model
{
	public class UnitModel : UnitModelBase, IModelWithVIew<UnitView>, IUpdatable, IPhysics
	{
		public static event Action<UnitModel> UnitDeath;
		public UnitView View => ThisView;
		protected UnitView ThisView;
		
		public Color Color { get; protected set; }
		
		public void SetColor(Color color) => Color = color;

		public UnitModel InitModel(SettingsModel settings)
		{
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
			Object.Destroy(View.gameObject);
			UnitDeath?.Invoke(this);
		}
	}
}