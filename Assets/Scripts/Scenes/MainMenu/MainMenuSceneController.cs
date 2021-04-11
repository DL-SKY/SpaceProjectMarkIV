using SpaceProject.Services;
using SpaceProject.UI.Windows.MainMenu;
using SpaceProject.UI.WindowsManager;
using UnityEngine;

namespace SpaceProject.Scenes.MainMenu
{
    public class MainMenuSceneController : MonoBehaviour
    {
        private void Awake()
        {
            var windowsManager = ComponentLocator.Resolve<WindowsManager>();
            windowsManager?.CreateWindow<MainMenuWindow>(MainMenuWindow.prefabPath, Enums.EnumWindowsLayer.Main);
        }
    }
}
