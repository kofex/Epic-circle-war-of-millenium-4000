using System;
using Scripts.Core.Model.Base;

namespace Scripts.Core.View
{
	public abstract class ViewBase<TModel> : CacheBehaviour where TModel : ModelBase
	{
		public TModel Model { get; protected set; }

		public virtual void SetModel(TModel model)
		{
			Model = model;
		}

		protected virtual void OnViewDestroy()
		{
		}

		private void OnDestroy()
		{
			OnViewDestroy();
		}
	}
}