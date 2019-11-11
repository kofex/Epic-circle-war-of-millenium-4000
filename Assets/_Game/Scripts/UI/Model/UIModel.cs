using Scripts.Core;
using Scripts.Core.Interfaces;
using Scripts.Core.Model;
using Scripts.Core.Model.Base;
using Scripts.UI.View;
using UnityEngine;

namespace Scripts.UI.Model
{
	public class UIModel : ModelBase, IModelWithVIew<UIView>
	{
		public UIView View { get; }
		
		public UIView SetView(Transform parent = null)
		{
			return View;
		}
	}
}