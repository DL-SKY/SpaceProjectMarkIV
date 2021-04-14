using System;
using UnityEngine;

namespace SpaceProject.UI.Windows
{
    public class WindowBase : MonoBehaviour
    {
        public Action OnInitialize;
        public Action<bool, WindowBase> OnClose;        

        [Header("Main Settings")]
        [SerializeField] protected bool canUseEsc;


        public virtual void Initialize(object _data)
        {
            OnInitialize?.Invoke();
        }

        public void Close(bool _result = false)
        {
            OnClose?.Invoke(_result, this);
            Destroy(gameObject);

            CustomClose(_result);
        }

        public void OnClickEsc()
        {
            if (canUseEsc)
                Close();

            CustomOnClickEsc();
        }


        protected virtual void CustomClose(bool _result) { }
        protected virtual void CustomOnClickEsc() { }
    }
}
