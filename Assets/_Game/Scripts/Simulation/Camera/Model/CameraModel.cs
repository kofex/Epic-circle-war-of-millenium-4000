
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
            var cameraPref = GameCore.GetModel<SettingsModel>().GameSettings.GetPefab<CameraView>();
            _thisView = Object.Instantiate(cameraPref, parent).GetComponent<CameraView>();
            CalculateHeight();
            return View;
        }

        public new CameraModel InitModel(float gameAreaWidth, float gameFieldHeight)
        {
            _gameAreaWidth = gameAreaWidth;
            _gameAreaHeight = gameFieldHeight;
            return this;
        }

        private void CalculateHeight()
        {
            View.ThisCamera.orthographicSize = _gameAreaHeight * 0.5f;
            View.transform.position = new Vector3(_gameAreaWidth * 0.5f, _gameAreaHeight * 0.5f, -5f);
        }
    }
}