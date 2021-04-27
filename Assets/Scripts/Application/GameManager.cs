using SpaceProject.Constants;
using SpaceProject.Events;
using SpaceProject.Patterns;
using SpaceProject.Services;
using SpaceProject.UI.Windows.FPS;
using SpaceProject.UI.Windows.Loading;
using SpaceProject.UI.WindowsManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceProject.Application
{
    public class GameManager : Singleton<GameManager>
    {
        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoadedHandler;


            //UnityEngine.Application.targetFrameRate = 60;
#if UNITY_ANDROID
            UnityEngine.Application.targetFrameRate = 60;
#endif


            Initialize();
        }

        protected override void CustomOnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoadedHandler;
        }


        public AsyncOperation LoadSceneAsync(string _sceneName)
        {
            return SceneManager.LoadSceneAsync(_sceneName);
        }

        public bool CheckCurrentScene(string _sceneName)
        {
            return SceneManager.GetActiveScene().name == _sceneName;
        }


        //TODO
        private void Initialize()
        {
            CreateFPSCounter();
        }

        private void CreateFPSCounter()
        {
            var windowsManager = ComponentLocator.Resolve<WindowsManager>();
            windowsManager.CreateWindow<GameLoadingWindow>(GameLoadingWindow.prefabPath, Enums.EnumWindowsLayer.Loading);
            windowsManager.CreateWindow<FPSWindow>(FPSWindow.prefabPath, Enums.EnumWindowsLayer.Special);
        }

        private void OnSceneLoadedHandler(Scene _scene, LoadSceneMode _loadSceneMode)
        {
            Debug.LogWarning("[GameManager] OnSceneLoadedHandler() " + _scene.name);
            EventManager.DispatchEvent(ConstantEventsName.ON_SCENE_LOADED, _scene.name);
        }
    }
}
