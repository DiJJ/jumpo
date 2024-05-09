using Cinemachine;
using UnityEngine;

namespace Main
{
    public class Level : MonoBehaviour {
        [SerializeField]
        private CinemachineVirtualCamera _camera;

        private void OnTriggerEnter2D(Collider2D other) {
            _camera.Follow = transform;
        }

#if UNITY_EDITOR || DEBUG
        private void OnValidate() {
            if (_camera == null)
                _camera = FindObjectOfType<CinemachineVirtualCamera>();
        }
#endif
    }
}
