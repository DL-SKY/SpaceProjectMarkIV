using SpaceProject.Application;
using SpaceProject.Constants;
using SpaceProject.Services;
using SpaceProject.UI.Windows.Loading;
using SpaceProject.UI.Windows.Test;
using UnityEngine;

namespace SpaceProject.UI.Windows.MainMenu
{
    public class MainMenuWindow : WindowBase
    {
        public const string prefabPath = @"Prefabs\UI\Windows\MainMenu\MainMenuWindow";

        private object data;
        private GameManager gameManager;
        private AsyncOperation loading;


        private void Awake()
        {
            gameManager = GameManager.Instance;
        }


        public override void Initialize(object _data)
        {
            data = _data;

            if (gameManager.CheckCurrentScene(ConstantScenes.MAIN_MENU))
            {
                base.Initialize(_data);
            }
            else
            {
                loading = gameManager.LoadSceneAsync(ConstantScenes.MAIN_MENU);
                loading.completed += OnLoadSceneCompletedHandler;
            }
        }

        public void OnClickTest()
        {
            var windowsManager = ComponentLocator.Resolve<WindowsManager.WindowsManager>();
            windowsManager.CreateWindow<TestShootingWindow>(TestShootingWindow.prefabPath, Enums.EnumWindowsLayer.Main);

            Close();
        }

        public void OnClickTest2()
        {
            var windowsManager = ComponentLocator.Resolve<WindowsManager.WindowsManager>();
            windowsManager.CreateWindow<SpaceShipTestWindow>(SpaceShipTestWindow.prefabPath, Enums.EnumWindowsLayer.Main);

            Close();
        }


        private void OnLoadSceneCompletedHandler(AsyncOperation _operation)
        {
            if (loading != null)
                loading.completed -= OnLoadSceneCompletedHandler;

            base.Initialize(data);
        }
    }
}
