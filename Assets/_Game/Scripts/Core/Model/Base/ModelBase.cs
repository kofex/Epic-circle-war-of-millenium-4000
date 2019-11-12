using Scripts.Core.Interfaces;

namespace Scripts.Core.Model.Base
{
	public abstract class ModelBase : IInitiable<ModelBase>
	{
		public static T CreateModel<T>() where T : ModelBase, new()
		{
			return new T();
		}

		public virtual ModelBase InitModel()
		{
			return this;
		}
	}
}