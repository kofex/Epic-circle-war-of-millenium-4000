using Scripts.Core.Model.Base;
using Scripts.Core.View;
using UnityEngine;

namespace Scripts.UI.View
{
	[RequireComponent(typeof(Canvas))]
	public class UIView<TModel> : ViewBase<ModelBase> where TModel : ModelBase 
	{
		private Canvas _canvas;
		public Canvas Canvas => _canvas != null ? _canvas : _canvas = GetComponent<Canvas>();
		
		public new TModel Model { get; protected set; }

		public new TModel SetModel(TModel model)
		{
			return Model = model;
		}

	}
}