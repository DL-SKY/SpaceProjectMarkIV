using SpaceProject.Enums;
using SpaceProject.InputSystem.Adapters;
using SpaceProject.Tools.Components;
using System.Collections.Generic;

namespace SpaceProject.InputSystem
{
    public class InputController : AutoLocatorObject
    {
        private Dictionary<EnumInputAdapters, InputAdapter> adapters = new Dictionary<EnumInputAdapters, InputAdapter>();


        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            foreach (var adapter in adapters)
                adapter.Value.Update();
        }


        public void Initialize()
        {
            adapters.Clear();

            adapters.Add(EnumInputAdapters.GameplayCamera, GetGameplayCameraAdapter());
        }

        public void SetAdapterEnable(EnumInputAdapters _adapter, bool _state)
        {
            if (adapters.ContainsKey(_adapter))
                adapters[_adapter].SetEnable(_state);
        }


        private InputAdapter GetGameplayCameraAdapter()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            return new GameplayCameraStandaloneAdapter(true);
#else
#endif
        }
    }
}
