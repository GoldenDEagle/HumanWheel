using UnityEngine;

namespace Assets.Codebase.Optimizations.Cameras
{
    public class TitleScreenCameraController : MonoBehaviour
    {
        [SerializeField] private float _verticalOrthSize = 12f;
        [SerializeField] private float _horizontalOrthSize = 6f;

        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (Screen.width >= Screen.height)
            {
                _mainCamera.orthographicSize = _horizontalOrthSize;
            }
            else
            {
                _mainCamera.orthographicSize = _verticalOrthSize;
            }
        }
    }
}