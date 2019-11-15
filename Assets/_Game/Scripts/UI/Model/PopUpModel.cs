using System;
using Scripts.Core;
using Scripts.Simulation.Model;
using Scripts.UI.View;
using UnityEngine;

namespace Scripts.UI.Model
{
	public class PopUpModel : UIModel<PopUpView>
	{
		public static Action NewButtonClick;
		private const string NEW_BTN = "NEW SIMULATION";
		private const string TIME_TEXT = "Simulation time:";

		private MainUIModel _mainUIModel;

		public new PopUpView SetView(Transform parent = null)
		{
			ThisView = _mainUIModel.View.PopUp;
			View.NewGameBtn.AddListener(()=>NewButtonClick?.Invoke());
			View.SetModel(this);
			View.NewGameBtn.SetTest(NEW_BTN);
			UnPop();
			return View;
		}

		public PopUpModel InitModel(MainUIModel mainUIModel)
		{
			NewButtonClick += () =>
			{
				UnPop();
				GameCore.GetModel<SimulationModel>().Restart();
			};
			_mainUIModel = mainUIModel;
			return this;
		}

		public void Pop(float simulationTime, Color winColor)
		{
			View.Canvas.enabled = true;
			SetUpResults(simulationTime, winColor);
		}

		public void UnPop() => View.Canvas.enabled = false;
		private void SetActive(bool isActive) => View.Canvas.enabled = isActive;

		private void SetUpResults(float simulationTime, Color winColor)
		{
			View.TimeText.text = $"{TIME_TEXT} {simulationTime} sec";
			View.WinColorSprite.color = winColor;
		}
		

	}
}