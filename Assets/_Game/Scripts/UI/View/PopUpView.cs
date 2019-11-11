using Scripts.Core.Model.Base;
using Scripts.UI.Components;
using Scripts.UI.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.View
{
	public class PopUpView : UIView<PopUpModel> 
	{
		[SerializeField] private ButtonWithText _newGameBtn;
		public ButtonWithText NewGameBtn => _newGameBtn;
		
		[SerializeField] private Image _winColoSprite;
		public Image WinColorSprite => _winColoSprite;
		
		[SerializeField] private Text _timeText;
		public Text TimeText => _timeText;
		

		protected override void OnViewDestroy()
		{
			NewGameBtn.RemoveAllListeners();
		}
	}
}