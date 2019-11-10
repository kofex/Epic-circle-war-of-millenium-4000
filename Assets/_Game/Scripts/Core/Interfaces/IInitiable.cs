using Scripts.Core.Model.Base;

namespace Scripts.Core.Interfaces
{
	public interface IInitiable<out TModel> where TModel : ModelBase
	{
		TModel InitModel();

	}
}