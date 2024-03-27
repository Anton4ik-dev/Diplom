using UnityEngine;

namespace RogueHelper.Services
{
    [ExecuteInEditMode]
    public class CameraScaler : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private int targetWidth = 640;
        [SerializeField] private float pixelToUnits = 20;

        private Vector2 _resolution;

        private void Awake()
        {
            if (_mainCamera is null)
                _mainCamera = Camera.main;
            ResizeCamera();
            _resolution = new Vector2(Screen.width, Screen.height);
        }

        private void Update()
        {
            ResizeCamera();
        }

        private void ResizeCamera()
        {
            if (_resolution.x != Screen.width)
            {
                int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);
                _mainCamera.orthographicSize = height / pixelToUnits / 2;

                _resolution.x = Screen.width;
            }

            if (_resolution.y != Screen.height)
            {
                _resolution.y = Screen.height;
            }
        }
    }
}