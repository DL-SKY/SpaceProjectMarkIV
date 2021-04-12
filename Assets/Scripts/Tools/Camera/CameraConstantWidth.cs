using SpaceProject.Constants;
using SpaceProject.Events;
using UnityEngine;

namespace SpaceProject.Tools.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraConstantWidth : MonoBehaviour
    {
        [SerializeField] private Vector2 defaultResolution = new Vector2(1920, 1080);
        [Range(0f, 1f)]
        [SerializeField] private float widthOrHeight = 0.0f;

        private new UnityEngine.Camera camera;

        private float initialSize;
        private float targetAspect;

        private float initialFov;
        private float horizontalFov = 120.0f;


        private void Start()
        {
            EventManager.AddEventListener(ConstantEventsName.ON_RESOLUTION_CHANGE, OnResolutionChangeHandler);

            Initialize();
            ApplySettings();
        }

        private void OnDestroy()
        {
            EventManager.RemoveEventListener(ConstantEventsName.ON_RESOLUTION_CHANGE, OnResolutionChangeHandler);
        }


        public void ApplySettings()
        {
            if (camera.orthographic)
            {
                float constantWidthSize = initialSize * (targetAspect / camera.aspect);
                camera.orthographicSize = Mathf.Lerp(constantWidthSize, initialSize, widthOrHeight);
            }
            else
            {
                float constantWidthFov = CalcVerticalFov(horizontalFov, camera.aspect);
                camera.fieldOfView = Mathf.Lerp(constantWidthFov, initialFov, widthOrHeight);
            }
        }


        private void Initialize()
        {
            camera = GetComponent<UnityEngine.Camera>();
            initialSize = camera.orthographicSize;

            targetAspect = defaultResolution.x / defaultResolution.y;

            initialFov = camera.fieldOfView;
            horizontalFov = CalcVerticalFov(initialFov, 1.0f / targetAspect);
        }        

        private float CalcVerticalFov(float _hFovInDeg, float _aspectRatio)
        {
            float hFovInRads = _hFovInDeg * Mathf.Deg2Rad;
            float vFovInRads = 2.0f * Mathf.Atan(Mathf.Tan(hFovInRads / 2.0f) / _aspectRatio);

            return vFovInRads * Mathf.Rad2Deg;
        }

        private void OnResolutionChangeHandler(CustomEvent _event)
        {
            ApplySettings();
        }
    }
}
