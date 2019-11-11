
using Scripts.Core;
using Scripts.Core.Interfaces;
using Scripts.Core.Model;
using Scripts.Core.Model.Base;
using Scripts.Simulation.Camera.View;
using UnityEngine;

namespace Scripts.Simulation.Camera.Model
{
    public class CameraModel : ModelBase, IModelWithVIew<CameraView>
    {
        private CameraView _thisView;
        public CameraView View => _thisView;

        private float _gameAreaWidth;
        private float _gameAreaHeight;
        
        public CameraView SetView(Transform parent = null)
        {
            var settings = GameCore.GetModel<SettingsModel>().GameSettings;
            var cameraPref = settings.GetPefab<CameraView>();
            _thisView = Object.Instantiate(cameraPref, parent);
            CalculateOrthSize(settings.UiOffset);
            return View;
        }

        public new CameraModel InitModel(float gameAreaWidth, float gameFieldHeight)
        {
            _gameAreaWidth = gameAreaWidth;
            _gameAreaHeight = gameFieldHeight;
            return this;
        }

        private void CalculateOrthSize(float uiOffset = 0)
        {
            View.ThisCamera.orthographicSize = _gameAreaHeight * 0.5f + uiOffset; 
            View.transform.position = new Vector3(_gameAreaWidth * 0.5f, _gameAreaHeight * 0.5f, -5f);
        }
    }
}