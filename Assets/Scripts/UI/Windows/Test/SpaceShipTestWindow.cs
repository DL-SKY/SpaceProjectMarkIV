using SpaceProject.Application;
using SpaceProject.Constants;
using SpaceProject.Services;
using SpaceProject.UI.Windows.Loading;
using SpaceProject.UI.Windows.MainMenu;

namespace SpaceProject.UI.Windows.Test
{
    public class SpaceShipTestWindow : WindowBase
    {
        public const string prefabPath = @"Prefabs\UI\Windows\Test\SpaceShipTestWindow";

        private object data;
        private GameManager gameManager;
        private SceneLoadingWindow loadingWindow;


        private void Awake()
        {
            gameManager = GameManager.Instance;
        }


        public override void Initialize(object _data)
        {
            data = _data;

            if (gameManager.CheckCurrentScene(ConstantScenes.SPACE_TEST))
            {
                base.Initialize(_data);
            }
            else
            {
                var windowsManager = ComponentLocator.Resolve<WindowsManager.WindowsManager>();
                loadingWindow = windowsManager.CreateWindow<SceneLoadingWindow>(SceneLoadingWindow.prefabPath, Enums.EnumWindowsLayer.Loading, ConstantScenes.SPACE_TEST);
                loadingWindow.OnSceneLoaded += OnSceneLoadedHandler;



                //loading = gameManager.LoadSceneAsync(ConstantScenes.SPACE_TEST);
                //loading.completed += OnLoadSceneCompletedHandler;
            }
        }


        protected override void CustomClose(bool _result)
        {
            var windowsManager = ComponentLocator.Resolve<WindowsManager.WindowsManager>();
            windowsManager.CreateWindow<MainMenuWindow>(MainMenuWindow.prefabPath, Enums.EnumWindowsLayer.Main);
        }


        private void OnSceneLoadedHandler()
        {
            if (loadingWindow != null)
            {
                loadingWindow.OnSceneLoaded -= OnSceneLoadedHandler;
                loadingWindow.Close();
            }

            base.Initialize(data);            
        }
    }
}
