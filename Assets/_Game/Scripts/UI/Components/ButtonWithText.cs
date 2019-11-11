using Scripts.Core.View;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.UI.Components
{
	[RequireComponent(typeof(Button), typeof(Image))]
	public class ButtonWithText : CacheBehaviour
	{
		private Button _button;
		public Button Button => _button != null ? _button : _button = GetComponent<Button>();
		
		[SerializeField]
		private Text _text;
		public Text Text => _text;

		public void SetTest(string str) => Text.text = str;
		
		public void AddListener(UnityAction action) => Button.onClick.AddListener(action);

		public void RemoveListener(UnityAction action) => Button.onClick.RemoveListener(action);
		
		public void RemoveAllListeners() => Button.onClick.RemoveAllListeners();
	}
}