using SpaceProject.Constants;
using SpaceProject.Patterns;
using SpaceProject.Services;
using SpaceProject.UI.WindowsManager;
using SpaceProject.UI.Windows.Loading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using SpaceProject.UI.Windows.FPS;

namespace SpaceProject.Application
{
    public class GameManager : Singleton<GameManager>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            Initialize();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
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
            var windowsManager = ComponentLocator.Resolve<WindowsManager>();
            windowsManager.CreateWindow<GameLoadingWindow>(GameLoadingWindow.prefabPath, Enums.EnumWindowsLayer.Loading);
            windowsManager.CreateWindow<FPSWindow>(FPSWindow.prefabPath, Enums.EnumWindowsLayer.Special);
        }


    }
}
