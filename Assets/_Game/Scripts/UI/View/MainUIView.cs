using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.View
{
	public class MainUIView : UIView
	{
		[SerializeField] private Image _teamBack;
		public Image TeamBack => _teamBack;
		[SerializeField] private Image _teamFront;
		public Image TeamFront =>_teamFront;
		[SerializeField] private Button _newGameBtn;
		public Button NewGameBtn => _newGameBtn;
		[SerializeField] private Button _saveGameBtn;
		public Button SaveGameBtn => _saveGameBtn;
		[SerializeField] private Button _loadGameBtn;
		public Button LoadGameBtn => _loadGameBtn;
		[SerializeField] private PopUpView _popUp;
		public PopUpView PopUp => _popUp;
	}
}