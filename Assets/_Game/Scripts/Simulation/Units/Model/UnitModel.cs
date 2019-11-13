using System;
using Scripts.Core.Interfaces;
using Scripts.Core.Model;
using Scripts.Physics.Interface;
using Scripts.Serialization.Containers;
using Scripts.Simulation.Model;
using Scripts.UI.Model;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.Simulation.Units.Model
{
	public class UnitModel : UnitModelBase, IModelWithVIew<UnitView>, IUpdatable, IPhysics, IRestartable, ISerializableContainer<UnitSerializationContainer>
	{
		public static event Action<UnitModel> UnitDeath;
		protected static int ID;
		
		public UnitView View => ThisView;
		protected Color Color { get; private set; }
		public void SetColor(Color color) => Color = color;
		protected bool IsDead { get; private set; }
		
		protected UnitView ThisView;

		private new void InitModel()
		{
		}

		public static void RestId()=> ID = 0;

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
			IsDead = true;
			UnitDeath?.Invoke(this);
			Object.Destroy(View.gameObject);
		}

		public void SetDefault()
		{
		}

		public virtual void Restart()
		{
			SetDefault();
			Object.Destroy(View.gameObject);	
		}

		public virtual UnitSerializationContainer Serialize()
		{
			return IsDead || View == null ? null : new UnitSerializationContainer();
		}

		public virtual void Deserialize(UnitSerializationContainer container)
		{
		}
	}
}