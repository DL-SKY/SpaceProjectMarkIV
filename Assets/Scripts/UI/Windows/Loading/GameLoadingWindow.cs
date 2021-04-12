using SpaceProject.Services;
using SpaceProject.UI.Windows.MainMenu;
using System.Collections;
using UnityEngine;

namespace SpaceProject.UI.Windows.Loading
{
    public class GameLoadingWindow : WindowBase
    {
        public const string prefabPath = @"Prefabs\UI\Windows\Loading\GameLoadingWindow";

        //private string sceneName;
        private MainMenuWindow mainMenu;


        public override void Initialize(object _data)
        {
            //sceneName = (string)_data;

            //var operation = SceneManager.LoadSceneAsync(sceneName);
            //StartCoroutine(UpdateProgress(operation));

            var windowsManager = ComponentLocator.Resolve<WindowsManager.WindowsManager>();
            mainMenu = windowsManager.CreateWindow<MainMenuWindow>(MainMenuWindow.prefabPath, Enums.EnumWindowsLayer.Main);
            mainMenu.OnInitialize += OnInitializeHandler;

            base.Initialize(_data);
        }


        private void OnInitializeHandler()
        {
            mainMenu.OnInitialize -= OnInitializeHandler;
            Close();
        }

        //private IEnumerator UpdateProgress(AsyncOperation _operation)
        //{
        //    if (_operation == null)
        //        yield break;

        //    while (!_operation.isDone)
        //    {
        //        Debug.LogError("Progress : " + _operation.progress);
        //        yield return null;
        //    }

        //    //yield return new WaitForSeconds(2.5f);

        //    Close();
        //}
    }
}
