using SpaceProject.Enums;
using SpaceProject.InputSystem.Adapters;
using SpaceProject.Tools.Components;
using System.Collections.Generic;

namespace SpaceProject.InputSystem
{
    public class InputController : AutoLocatorObject
    {
        private Dictionary<EnumInputAdapters, InputAdapter> adapters = new Dictionary<EnumInputAdapters, InputAdapter>();

#if UNITY_EDITOR
        private Dictionary<EnumInputAdapters, InputAdapter> editorAdapters = new Dictionary<EnumInputAdapters, InputAdapter>();
#endif


        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            foreach (var adapter in adapters)
                adapter.Value.Update();

#if UNITY_EDITOR
            foreach (var adapter in editorAdapters)
                adapter.Value.Update();
#endif
        }


        public void Initialize()
        {
            adapters.Clear();

            //Добавляем необходимые адаптеры
            adapters.Add(EnumInputAdapters.SpaceshipControl, GetSpaceshipControlAdapter());

#if UNITY_EDITOR
            editorAdapters.Clear();
            editorAdapters.Add(EnumInputAdapters.SpaceshipControl, new SpaceshipControlEditorAdapter(true));
#endif
        }

        public void SetAdapterEnable(EnumInputAdapters _adapter, bool _state)
        {
            if (adapters.ContainsKey(_adapter))
                adapters[_adapter].SetEnable(_state);
        }


        private InputAdapter GetSpaceshipControlAdapter()
        {
#if UNITY_ANDROID
            return new SpaceshipControlMobileAdapter(true);
#else
            return null;
#endif
        }
    }
}

//#if UNITY_EDITOR || UNITY_STANDALONE
//#if UNITY_ANDROID
//#if UNITY_IPHONE