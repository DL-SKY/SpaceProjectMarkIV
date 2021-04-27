using SpaceProject.Constants;
using SpaceProject.Events;
using UnityEngine;

namespace SpaceProject.UI.WindowsManager
{
    public class WorldCanvasCameraChanger : MonoBehaviour
    {
        [SerializeField] private Canvas worldCanvas;


        private void OnEnable()
        {
            EventManager.AddEventListener(ConstantEventsName.ON_SCENE_LOADED, OnSceneLoadedHandler);
        }

        private void OnDisable()
        {
            EventManager.RemoveEventListener(ConstantEventsName.ON_SCENE_LOADED, OnSceneLoadedHandler);
        }


        private void OnSceneLoadedHandler(CustomEvent _event)
        {
            worldCanvas.worldCamera = Camera.main;
        }
    }
}
