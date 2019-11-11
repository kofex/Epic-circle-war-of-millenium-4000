using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.View
{
	public class PopUpView : UIView
	{
		[SerializeField] private Image _winColoSprite;
		public Image WinColorSprite => _winColoSprite;
		[SerializeField] private Text _timeText;
		public Text TimeText => _timeText;
		[SerializeField] private Button _newGameBtn;
		public Button NewGameBtn => _newGameBtn;
	}
}