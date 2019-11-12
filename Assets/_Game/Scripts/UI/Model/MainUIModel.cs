using System;
using Scripts.Core;
using Scripts.Core.Model;
using Scripts.Simulation.Model;
using Scripts.Simulation.Units.Model;
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
		private float _maxTeamBalanceWidth;
		
		public new MainUIModel InitModel()
		{
			TeamsModel<CircleUnitModel>.UnitCountChange += SetTeamsBalance; 
			return this;
		}
		
		public new MainUIView SetView(Transform parent = null)
		{
			var prefab = GameCore.GetModel<SettingsModel>().GameSettings.GetPefab<MainUIView>();
			ThisView = Object.Instantiate(prefab, parent);
			View.SetModel(this);
			var popUpModel = CreateModel<PopUpModel>().InitModel(this).SetView(View.transform).Model;
			_maxTeamBalanceWidth = View.TeamFront.rectTransform.sizeDelta.x;
			var teams = GameCore.GetModelFromSimulations<TeamsModel<CircleUnitModel>>().Teams;
			View.TeamFront.color = teams[0].TeamColor;
			View.TeamBack.color = teams[1].TeamColor;
			SetTeamsBalance(teams[0].Units.Count, teams[1].Units.Count);
			
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

		private void SetTeamsBalance(int teamCountOne, int teamCountTwo)
		{
			var rectTransform = View.TeamFront.rectTransform;
			var size = rectTransform.sizeDelta;
			size.x = _maxTeamBalanceWidth * teamCountOne / (teamCountOne + teamCountTwo);
			rectTransform.sizeDelta = size;
		}

	}
}