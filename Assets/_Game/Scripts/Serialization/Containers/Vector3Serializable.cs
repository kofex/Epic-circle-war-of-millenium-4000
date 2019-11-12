using System;
using Scripts.UI.Model;
using UnityEngine;

namespace Scripts.Serialization.Containers
{
	[Serializable]
	public class Vector3Serializable : IMySerializable
	{
		public float x;
		public float y;
		public float z;
		
		public Vector3Serializable(float sX, float sY, float sZ)
		{
			x = sX;
			y = sY;
			z = sZ;
		}
		
		public static implicit operator Vector3(Vector3Serializable sVector3)
		{
			return new Vector3(sVector3.x, sVector3.y, sVector3.z);
		}
		
		public static implicit operator Vector3Serializable(Vector3 vector3)
		{
			return new Vector3Serializable(vector3.x, vector3.y, vector3.z);
		}
		
		public static implicit operator Vector2(Vector3Serializable sVector3)
		{
			return new Vector2(sVector3.x, sVector3.y);
		}
		
		public static implicit operator Vector3Serializable(Vector2 vector2)
		{
			return new Vector3Serializable(vector2.x, vector2.y, 0f);
		}
	}
}