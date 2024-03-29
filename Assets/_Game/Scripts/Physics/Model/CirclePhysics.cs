using Scripts.Core;
using Scripts.Core.Model;
using Scripts.Simulation.Units.Model;
using UnityEngine;

namespace Scripts.Physics.Model
{
	public class CirclePhysics : PhysicsModel<CircleUnitModel>
	{
		private float _additionShrinkDisplacement;
		public new CirclePhysics InitModel()
		{
			_additionShrinkDisplacement = GameCore.GetModel<SettingsModel>().GameSettings.AdditionShrinkDisplacement;
			base.InitModel();
			return this;
		}
		
		protected override void CheckBorderCollision()
		{
			foreach (var unit in Units)
			{
				var unitPos = unit.View.transform.position;
				var resVec = Vector2.zero;

				var range = Random.Range(BorderBounceOffset.x, BorderBounceOffset.y);
				if (unitPos.x - unit.Radius < BorderRect.minX)
				{
					resVec += new Vector2(1f, range);
				}
				if (unitPos.x + unit.Radius> BorderRect.maxX)
				{
					resVec += new Vector2(-1f, range);
				}

				if(unitPos.y - unit.Radius <= BorderRect.minY)
				{
					resVec += new Vector2(range,1f);
				}

				if (unitPos.y + unit.Radius >= BorderRect.maxY)
				{
					resVec += new Vector2(range,-1f); 
				}


				if (resVec != Vector2.zero)
				{
					unit.OnCollision(resVec.normalized);
				}
			}
		}

		protected override void CheckUnitsCollision()
		{
			foreach (var unit in Units)
			{
				foreach (var target in Units)
				{
					if(unit == target)
						continue;

					var unitPos = unit.View.transform.position;
					var targetPost = target.View.transform.position;

					var distVector = GetDistance(unitPos, targetPost);
					var dist = distVector.magnitude;
					var radiiSum = unit.Radius + target.Radius;
					if (dist > radiiSum)
						continue;

					if (unit.Color == target.Color)
					{
						CollideSame(unit, target, distVector);
					}
					else
					{
						CollideDifferent(unit, target, distVector);
					}
				}
			}
		}
		

		private void CollideSame(CircleUnitModel unit, CircleUnitModel target, Vector2 distVector)
		{
			unit.OnCollision(distVector.normalized);
			target.OnCollision(-distVector.normalized);
		}

		private void CollideDifferent(CircleUnitModel unit, CircleUnitModel target, Vector2 distVector)
		{
			var shrink = distVector.magnitude * (1 + _additionShrinkDisplacement) - unit.Radius - target.Radius;
			unit.OnCollision(shrink, target);
			target.OnCollision(shrink, unit);
		}
	}
}