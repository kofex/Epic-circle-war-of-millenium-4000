using Scripts.Core;
using Scripts.Core.Model;
using Scripts.UI.View;
using UnityEngine;

namespace Scripts.UI.Model
{
	public class MainUIModel : UIModel
	{
		private MainUIView _thisView;
		public new MainUIView View => _thisView;
		
		public new MainUIModel InitModel()
		{
			return this;
		}
		
		public new MainUIView SetView(Transform parent = null)
		{
			_thisView = GameCore.GetModel<SettingsModel>().GameSettings.GetPefab<MainUIView>();
			return View;
		}

	}
}