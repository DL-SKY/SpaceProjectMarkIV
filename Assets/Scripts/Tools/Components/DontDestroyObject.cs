using UnityEngine;

namespace SpaceProject.Tools.Components
{
    public class DontDestroyObject : MonoBehaviour
    {
        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
