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


        private void OnEnable()
        {
            Debug.LogError("OnEnable");
        }


        public void OnClickTest()
        {
            //var windowsManager = ComponentLocator.Resolve<WindowsManager.WindowsManager>();
            //windowsManager.CreateWindow<GameLoadingWindow>(GameLoadingWindow.prefabPath, Enums.EnumWindowsLayer.Loading, ConstantScenes.TEST_SCENE);

            //Close();

            var windowsManager = ComponentLocator.Resolve<WindowsManager.WindowsManager>();
            windowsManager.CreateWindow<TestShootingWindow>(TestShootingWindow.prefabPath, Enums.EnumWindowsLayer.Main);

            Close();
        }        
    }
}
