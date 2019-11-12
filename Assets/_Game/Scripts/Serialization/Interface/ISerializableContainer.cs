using Scripts.Core.Model.Base;

namespace Scripts.UI.Model
{
	public interface ISerializableContainer<TContainer> where TContainer : IMySerializable
	{
		TContainer Serialize();
		void Deserialize(TContainer container);
		
	}
}