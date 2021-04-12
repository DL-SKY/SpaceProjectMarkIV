using SpaceProject.Application;
using System;
using UnityEngine;

namespace SpaceProject.UI.Windows.Loading
{
    public class SceneLoadingWindow : WindowBase
    {
        public const string prefabPath = @"Prefabs\UI\Windows\Loading\SceneLoadingWindow";

        public Action OnSceneLoaded;

        private string sceneName;
        private AsyncOperation loading;


        public override void Initialize(object _data)
        {
            sceneName = (string)_data;

            loading = GameManager.Instance.LoadSceneAsync(sceneName);
            loading.completed += OnLoadSceneCompletedHandler;

            base.Initialize(_data);
        }


        private void OnLoadSceneCompletedHandler(AsyncOperation _operation)
        {
            if (loading != null)
                loading.completed -= OnLoadSceneCompletedHandler;

            OnSceneLoaded?.Invoke();
        }
    }
}
