using UnityEngine;
using UnityEngine.Events;


namespace Yrr.UI.Core
{
    public abstract class UIScreen : MonoBehaviour, IScreen
    {
        [Space]
        [SerializeField] private UnityEvent<object> onShow;
        [SerializeField] private UnityEvent onHide;

        private IScreen.ICallback _callback;

        public void Show(object args, IScreen.ICallback callback)
        {
            _callback = callback;
            OnShow(args);
            onShow?.Invoke(args);
        }

        public void Hide()
        {
            OnHide();
            onHide?.Invoke();
        }

        public void Close()
        {
            if (_callback != null)
            {
                _callback.OnClose(this);
            }
        }

        protected virtual void OnShow(object args)
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}