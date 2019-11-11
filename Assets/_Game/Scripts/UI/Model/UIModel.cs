using Scripts.Core;
using Scripts.Core.Interfaces;
using Scripts.Core.Model;
using Scripts.Core.Model.Base;
using Scripts.Core.View;
using Scripts.UI.View;
using UnityEngine;

namespace Scripts.UI.Model
{
	public class UIModel<TView> : ModelBase, IModelWithVIew<TView> where TView : CacheBehaviour
	{
		protected TView ThisView;
		public TView View => ThisView;
		
		public TView SetView(Transform parent = null)
		{
			return View;
		}
	}
}