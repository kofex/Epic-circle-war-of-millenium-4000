using Scripts.Core;
using Scripts.Core.Model;
using Scripts.Physics.Model;
using Scripts.Serialization.Containers;
using Scripts.Simulation.Model;
using Scripts.Tools;
using Scripts.UI.Model;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Scripts.Simulation.Units.Model
{
	public class CircleUnitModel : UnitModel
	{
		public int UnitId { get; protected set; }
		public float Radius { get; protected set; }
		
		private float _radiusToDeath;
		
		
		public CircleUnitModel InitModel(SettingsModel settings)
		{
			var configs = settings.GameConfigs.GameConfig;
			Radius = Random.Range(configs.minUnitRadius, configs.maxUnitRadius);
			_radiusToDeath = settings.GameSettings.DeathRadius;
			var spedValue = Random.Range(configs.minUnitSpeed, configs.maxUnitSpeed);
			Speed = new Vector2().RandomNormalized() * spedValue;
			CurrentSpeed = Speed;
			UnitId = ID++;
			EndSetUp();
			return this;
		}

		private void EndSetUp()
		{
			Width = Height = Radius * 2f;
			TeamsModelBase.UpdateEnd += CheckForDeath;
			base.InitModel();
		}

		public override UnitView SetView(Transform parent = null)
		{
			var prefab = GameCore.GetModel<SettingsModel>().GameSettings.GetPefab<UnitView>();
			ThisView = Object.Instantiate(prefab, parent);
			ThisView.name = $"Circle {UnitId}";
			ThisView.SetModel(this);
			SetupUnit();
			return View;
		}

		private void SetupUnit()
		{
			View.transform.position = StartPosition;
			View.SpriteRenderer.color = Color;
			View.transform.localScale = new Vector3(Width, Height, View.transform.localScale.z);
		}

		private void CheckForDeath()
		{
			if (Radius > _radiusToDeath)
				return;
			
			OnDeath();
			TeamsModelBase.UpdateEnd -= CheckForDeath;
		}

		public override void OnCollision(Vector2 collisionReflection, object other = null)
		{
			CurrentSpeed = collisionReflection;
			CurrentSpeed *= Speed.magnitude;
			
			if (other == null || other.GetType() != GetType())
				return;
			
			var otherUnit = other as CircleUnitModel;
			if(otherUnit.Color == Color)
				return;
			
			var dist = PhysicsBase.GetDistance(View.transform.position, otherUnit.View.transform.position).magnitude;
			var shrink = dist - Radius - otherUnit.Radius;
			
			Radius += shrink;
			View.transform.localScale = new Vector3(Radius*2f, Radius*2f, View.transform.localScale.z);
		}

		public override UnitSerializationContainer Serialize()
		{
			return new UnitSerializationContainer(View.transform.position, CurrentSpeed, Speed, _radiusToDeath, Radius,
				UnitId);
		}

		public override void Deserialize(UnitSerializationContainer container)
		{
			StartPosition = container.Position;
			CurrentSpeed = container.CurrentSpeed;
			Speed = container.Speed;
			_radiusToDeath = container.RadiusToDeath;
			Radius = container.Radius;
			UnitId = ID;
			ID++;
			
			EndSetUp();
		}
	}
}