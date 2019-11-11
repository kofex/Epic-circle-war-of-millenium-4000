using Scripts.UI.Components;
using Scripts.UI.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.View
{
	public class MainUIView : UIView<MainUIModel>
	{
		[SerializeField] private Image _teamBack;
		public Image TeamBack => _teamBack;
		
		[SerializeField] private Image _teamFront;
		public Image TeamFront =>_teamFront;
		
		[SerializeField] private ButtonWithText _newGameBtn;
		public ButtonWithText NewGameBtn => _newGameBtn;
		
		[SerializeField] private ButtonWithText _saveGameBtn;
		public ButtonWithText SaveGameBtn => _saveGameBtn;
		
		[SerializeField] private ButtonWithText _loadGameBtn;
		public ButtonWithText LoadGameBtn => _loadGameBtn;
		
		[SerializeField] private PopUpView _popUp;
		public PopUpView PopUp => _popUp;
	}
}