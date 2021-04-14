using SpaceProject.Services;
using UnityEngine;

namespace SpaceProject.Tools.Components
{
    public class AutoLocatorObject : MonoBehaviour
    {
        [Tooltip("Если NULL - автоопределение")]
        [SerializeField] protected Component component;


        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);

            if (component == null)
                component = this;
            ComponentLocator.Register(component);

            CustomAwake();
        }

        protected void OnDestroy()
        {
            ComponentLocator.Unregister(component.GetType());

            CustomOnDestroy();
        }


        protected virtual void CustomAwake() { }
        protected virtual void CustomOnDestroy() { }
    }
}
