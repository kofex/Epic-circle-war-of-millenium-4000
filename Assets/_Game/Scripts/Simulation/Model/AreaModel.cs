using System;
using System.Collections.Generic;
using Scripts.Core;
using Scripts.Core.Interfaces;
using Scripts.Core.Model;
using Scripts.Core.Model.Base;
using Scripts.Simulation.Units.Model;
using Scripts.Simulation.View;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Scripts.Simulation.Model
{
	public class AreaModel : ModelBase, IModelWithVIew<AreaView>
	{
		public static event Action UnitsSpawned;
		public static event Action<BorderRect> BordersCreated;

		public AreaView View => ThisView;

		protected AreaView ThisView { get; set; }

		private BorderRect BorderRect { get; set; }
		private Vector2 _offset;
		//TODO: Подумать над спавном только в свободных местах
		private List<Vector4> _bannedPositions;

		public AreaView SetView(Transform parent)
		{
			var prefab = GameCore.GetModel<SettingsModel>().GameSettings.GetPefab<AreaView>();
			ThisView = Object.Instantiate(prefab, parent).GetComponent<AreaView>();
			ThisView.SetModel(this);
			ConstructArea();
			return View;
		}

		public new AreaModel InitModel()
		{
			var gameRes = GameCore.GetModel<SettingsModel>();
			var config = gameRes.GameConfigs.GameConfig;
			BorderRect = new BorderRect
			{
				minX = 0.0f,
				maxX = config.gameAreaWidth,
				minY = 0.0f,
				maxY = config.gameAreaHeight,
			};
			
			BordersCreated?.Invoke(BorderRect);

			_offset = gameRes.GameSettings.BorderOffset;

			return this;
		}

		private void ConstructArea()
		{
			View.transform.localScale = new Vector3(BorderRect.maxX, BorderRect.maxY, 1f);
		}

		public void SpawnUnit(UnitModel unit)
		{
			if (unit == null)
			{
				UnitsSpawned?.Invoke();
				return;
			}
			unit.SetPosition(GetRandomPosWithinBorder(unit.Width * 0.5f, unit.Height * 0.5f));
			unit.SetView();
		}
			
		/// <summary>
		/// Only for units with central pivot 
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="unitWidth"></param>
		/// <param name="unitHeight"></param>
		private Vector2 GetRandomPosWithinBorder(float unitHalfWidth, float unitHalfHeight)
		{
			var xMin = BorderRect.minX + unitHalfWidth + _offset.x;
			var xMax = BorderRect.maxX - unitHalfWidth - _offset.x;

			var yMin = BorderRect.minY + unitHalfHeight + _offset.y;
			var yMax = BorderRect.maxY - unitHalfHeight - _offset.y;

			return new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
		}

	}
}