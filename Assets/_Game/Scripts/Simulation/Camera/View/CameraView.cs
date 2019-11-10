using Scripts.Core.View;
using Scripts.Simulation.Camera.Model;

namespace Scripts.Simulation.Camera.View
{
    public class CameraView : ViewBase<CameraModel>
    {
        private UnityEngine.Camera _thisCamera;
        public UnityEngine.Camera ThisCamera => _thisCamera != null ? _thisCamera : (_thisCamera = GetComponent<UnityEngine.Camera>());


    }
}