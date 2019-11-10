using UnityEngine;

namespace Scripts.Core.View
{
    public class CacheBehaviour : MonoBehaviour
    {
        private Transform _transform;
        public new Transform transform => _transform == null ? (_transform = base.transform) : _transform;
    }
}
