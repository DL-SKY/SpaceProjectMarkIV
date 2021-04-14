using SpaceProject.Services;
using SpaceProject.UI.Windows.MainMenu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceProject.UI.Windows.Test
{
    public class TestShootingWindow : WindowBase
    {
        public const string prefabPath = @"Prefabs\UI\Windows\Test\TestShootingWindow";

        [Space()]
        [SerializeField] private TestShootingTouchController touchController;


        public override void Initialize(object _data)
        {
            touchController.ResetProgress();

            base.Initialize(_data);
        }


        protected override void CustomClose(bool _result)
        {
            var windowsManager = ComponentLocator.Resolve<WindowsManager.WindowsManager>();
            windowsManager.CreateWindow<MainMenuWindow>(MainMenuWindow.prefabPath, Enums.EnumWindowsLayer.Main);
        }
    }
}
