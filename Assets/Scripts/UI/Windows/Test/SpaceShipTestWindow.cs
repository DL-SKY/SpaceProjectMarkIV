using SpaceProject.Application;
using SpaceProject.Constants;
using SpaceProject.Enums;
using SpaceProject.Events;
using SpaceProject.Objects.Spaceship;
using SpaceProject.Services;
using SpaceProject.UI.Windows.Loading;
using SpaceProject.UI.Windows.MainMenu;
using System;
using UnityEngine;

namespace SpaceProject.UI.Windows.Test
{
    [Serializable]
    public class SpaceshipUIElementPrefab
    {
        public EnumSpaceshipUIElement uiElement;
        public GameObject prefab;
    }


    public class SpaceShipTestWindow : WindowBase
    {
        public const string prefabPath = @"Prefabs\UI\Windows\Test\SpaceShipTestWindow";

        [Space()]
        [SerializeField] private Transform contentHolder;
        [SerializeField] private SpaceShipControlPanel controlPanel;

        private object data;
        private GameManager gameManager;
        private SceneLoadingWindow loadingWindow;
        private SpaceShipTest3DUIWindow uiWindow;


        private void Awake()
        {
            gameManager = GameManager.Instance;

            EventManager.AddEventListener(ConstantEventsName.ON_PLAYER_SPACESHIP_CREATE, OnPlayerSpaceshipCreateHandler);
            EventManager.AddEventListener(ConstantEventsName.ON_PLAYER_SPACESHIP_DESTROY, OnPlayerSpaceshipDestroyHandler);
        }

        private void OnDestroy()
        {
            EventManager.RemoveEventListener(ConstantEventsName.ON_PLAYER_SPACESHIP_CREATE, OnPlayerSpaceshipCreateHandler);
            EventManager.RemoveEventListener(ConstantEventsName.ON_PLAYER_SPACESHIP_DESTROY, OnPlayerSpaceshipDestroyHandler);
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

        private void OnPlayerSpaceshipCreateHandler(CustomEvent _event)
        {
            var playerSpaceship = (PlayerSpaceship)_event.EventData;

            Create2DUIControl(playerSpaceship);
            Create3DUIWindows(playerSpaceship);
        }

        private void Create3DUIWindows(PlayerSpaceship _playerSpaceship)
        {
            var windowsManager = ComponentLocator.Resolve<WindowsManager.WindowsManager>();
            uiWindow = windowsManager.CreateWindow<SpaceShipTest3DUIWindow>(SpaceShipTest3DUIWindow.prefabPath, Enums.EnumWindowsLayer.World, _playerSpaceship, false);
        }

        private void Create2DUIControl(PlayerSpaceship _playerSpaceship)
        {
            controlPanel.Initialize(_playerSpaceship);
        }

        private void OnPlayerSpaceshipDestroyHandler(CustomEvent _event)
        {
            uiWindow?.Close();
        }
    }
}
