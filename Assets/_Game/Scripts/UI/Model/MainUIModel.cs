using System;
using Scripts.Core;
using Scripts.Core.Model;
using Scripts.Simulation.Model;
using Scripts.UI.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.UI.Model
{
	public class MainUIModel : UIModel<MainUIView>
	{
		public static Action NewBtnClick, SaveBtnClick, LoadBtnClick;
		
		private const string NEW_BTN = "NEW";
		private const string SAVE_BTN = "SAVE";
		private const string LOAD_BTN = "LOAD";
		
		public new MainUIModel InitModel()
		{
			return this;
		}
		
		public new MainUIView SetView(Transform parent = null)
		{
			var prefab = GameCore.GetModel<SettingsModel>().GameSettings.GetPefab<MainUIView>();
			ThisView = Object.Instantiate(prefab, parent);
			View.SetModel(this);
			var popUpModel = CreateModel<PopUpModel>().InitModel(this).SetView(View.transform).Model;
			
			SimulationModel.SimulationEnd += popUpModel.Pop;
			SetButtons();
			return View;
		}

		private void SetButtons()
		{
			View.NewGameBtn.AddListener(() => NewBtnClick?.Invoke());
			View.NewGameBtn.SetTest(NEW_BTN);
			View.SaveGameBtn.AddListener(() => SaveBtnClick?.Invoke());
			View.SaveGameBtn.SetTest(SAVE_BTN);
			View.LoadGameBtn.AddListener(() => LoadBtnClick?.Invoke());
			View.LoadGameBtn.SetTest(LOAD_BTN);
		}

		private void SetTeamsBalance()
		{
			//TODO: Динамическая полоска сил
		}

	}
}