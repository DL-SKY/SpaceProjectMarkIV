using UnityEngine;

namespace SpaceProject.Patterns
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;


        public static T Instance
        {
            get
            {
                if (instance != null)
                    return instance;

                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                }
                if (instance == null)
                {
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }

                return instance;
            }
        }

        public static bool IsInstantiated
        {
            get
            {
                return instance != null;
            }
        }


        protected void Awake()
        {
            if (!IsInstantiated)
                instance = GetComponent<T>();

            CustomAwake();
        }

        protected void OnDestroy()
        {
            if (instance == this)
                instance = null;

            CustomOnDestroy();
        }

        protected virtual void CustomAwake() { }
        protected virtual void CustomOnDestroy() { }
    }
}
