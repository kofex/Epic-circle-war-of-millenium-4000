using System;
using Scripts.UI.Model;

namespace Scripts.Serialization.Containers
{
	[Serializable]
	public class UnitSerializationContainer : IMySerializable
	{
		
		public Vector3Serializable Position;
		public Vector3Serializable CurrentSpeed;
		public Vector3Serializable Speed;
		public float RadiusToDeath;
		public float Radius;
		public int Id;
		
		
		public UnitSerializationContainer()
		{
			
		}
		
		public UnitSerializationContainer(Vector3Serializable position, Vector3Serializable currentSpeed, 
			Vector3Serializable speed, float radiusToDeath, float radius, int id)
		{
			Position = position;
			CurrentSpeed = currentSpeed;
			Speed = speed;
			RadiusToDeath = radiusToDeath;
			Radius = radius;
			Id = id;
		}
		
	}
}