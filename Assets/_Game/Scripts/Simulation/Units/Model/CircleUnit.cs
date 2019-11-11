using Scripts.Core;
using Scripts.Core.Model;
using Scripts.Physics.Model;
using Scripts.Simulation.Model;
using Scripts.Tools;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Scripts.Simulation.Units.Model
{
	public class CircleUnitModel : UnitModel
	{
		public int UnitId { get; protected set; }
		public float Radius { get; protected set; }
		private float radiusToDeath;
		
		
		public new CircleUnitModel InitModel(SettingsModel settings)
		{
			var configs = settings.GameConfigs.GameConfig;
			Radius = Random.Range(configs.minUnitRadius, configs.maxUnitRadius);
			radiusToDeath = settings.GameSettings.DeathRadius;
			var spedValue = Random.Range(configs.minUnitSpeed, configs.maxUnitSpeed);
			Speed = new Vector2().RandomNormalized() * spedValue;
			CurrentSpeed = Speed;
			Width = Height = Radius * 2f;
			TeamModelBase.UpdateEnd += CheckForDeath;
			base.InitModel(settings);
			return this;
		}
		
		public override UnitView SetView(Transform parent = null)
		{
			var prefab = GameCore.GetModel<SettingsModel>().GameSettings.GetPefab<UnitView>();
			ThisView = Object.Instantiate(prefab, parent);
			ThisView.name = $"Circle {UnitId = ID++}";
			ThisView.SetModel(this);
			SetupUnit();
			return View;
		}

		private void SetupUnit()
		{
			View.transform.position = Position;
			View.SpriteRenderer.color = Color;
			View.transform.localScale = new Vector3(Width, Height, View.transform.localScale.z);
		}

		private void CheckForDeath()
		{
			if (Radius > radiusToDeath)
				return;
			
			OnDeath();
			TeamModelBase.UpdateEnd -= CheckForDeath;
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
		
	}
}