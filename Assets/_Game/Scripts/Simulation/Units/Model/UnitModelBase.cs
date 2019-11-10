using Scripts.Core.Model.Base;
using UnityEngine;

namespace Scripts.Simulation.Units.Model
{
	public class UnitModelBase : ModelBase
	{
		public Vector2 Position { get; protected set; }
		public Vector2 Scale { get; protected set; }
		public Vector2 Speed { get; protected set; }
		public float Width { get; protected set; }
		public float Height { get; protected set; }
		
		protected Vector2 CurrentSpeed;

		protected new void InitModel()
		{
		}


		public void SetPosition(Vector2 pos) => Position = pos;
	}
}