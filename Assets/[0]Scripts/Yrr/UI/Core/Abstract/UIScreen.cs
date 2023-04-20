using UnityEngine;
using UnityEngine.Events;
using Yrr.UI.Core;


namespace Yrr.UI
{
    public abstract class UIScreen : MonoBehaviour, IScreen
    {
        [Space]
        [SerializeField] private UnityEvent<object> onShow;
        [SerializeField] private UnityEvent onHide;

        private IScreen.ICallback _callback;

        public void Show(object args, IScreen.ICallback callback)
        {
            ShowProcedure();
            _callback = callback;
            OnShow(args);
            onShow?.Invoke(args);
        }

        protected virtual void ShowProcedure()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            OnHide();
            onHide?.Invoke();
            HidingProcedure();
        }

        protected virtual void HidingProcedure()
        {
            gameObject.SetActive(false);
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