using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Scripts.Core.Model.Base;
using Scripts.UI.Model;


namespace Scripts.Serialization.Model
{
	public class SerializationModelBase : ModelBase
	{
		protected virtual void Serialize()
		{
		}

		
		protected virtual void Deserialize()
		{
		}

		protected string BinarySerialize(object toSerialize)
		{
			using (var stream = new MemoryStream())
			{
				var serializer = new BinaryFormatter();
				serializer.Serialize(stream, toSerialize);
				return Convert.ToBase64String(stream.ToArray());
			}
		}

		protected TObject BinaryDeserialization<TObject>(byte[] toDeserialize) where TObject : IMySerializable
		{
			using (var stream = new MemoryStream(toDeserialize))
			{
				var deserializer = new BinaryFormatter();
				return (TObject) deserializer.Deserialize(stream) ;
			}
		}
	}
}