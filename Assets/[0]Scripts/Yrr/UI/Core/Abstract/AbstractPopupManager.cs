using System;
using UnityEngine;


namespace Yrr.UI.Core
{
    public abstract class AbstractPopupManager<T> : MonoBehaviour, IPopupManager<T>
    {
        public event Action<T> OnPopupShown;
        public event Action<T> OnPopupHidden;

        protected abstract IScreenSupplier<T, UIScreen> Supplier { get; }

        private IPopupManager<T> _manager;

        protected virtual void Awake()
        {
            _manager = new InternalPopupManager<T, UIScreen>(Supplier);
        }

        protected virtual void OnEnable()
        {
            _manager.OnPopupShown += OnShowPopup;
            _manager.OnPopupHidden += OnHidePopup;
        }

        protected virtual void OnDisable()
        {
            _manager.OnPopupShown -= OnShowPopup;
            _manager.OnPopupHidden -= OnHidePopup;
        }


        public void ShowPopup(T key, object args = null)
        {
            _manager.ShowPopup(key, args);
        }

        public void HidePopup(T key)
        {
            _manager.HidePopup(key);
        }


        public void HideAllPopups()
        {
            _manager.HideAllPopups();
        }

        public bool IsPopupActive(T popupName)
        {
            return _manager.IsPopupActive(popupName);
        }

        private void OnShowPopup(T popupName)
        {
            OnPopupShown?.Invoke(popupName);
        }

        private void OnHidePopup(T popupName)
        {
            OnPopupHidden?.Invoke(popupName);
        }
    }
}