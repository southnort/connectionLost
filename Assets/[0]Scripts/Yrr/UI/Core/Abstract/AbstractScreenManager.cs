using System;
using UnityEngine;


namespace Yrr.UI.Core
{
    public abstract class AbstractScreenManager<T> : MonoBehaviour, IScreenManager<T>
    {
        public event Action<T> OnScreenShown;
        public event Action<T> OnScreenHidden;
        public event Action<T> OnScreenChanged;

        protected abstract IScreenSupplier<T, UIScreen> Supplier { get; }

        private IScreenManager<T> _manager;

        protected virtual void Awake()
        {
            _manager = new InternalScreenManager<T, UIScreen>(Supplier);
        }

        protected virtual void OnEnable()
        {
            _manager.OnScreenShown += OnShowScreen;
            _manager.OnScreenChanged += OnChangeScreen;
            _manager.OnScreenHidden += OnHideScreen;
        }

        protected virtual void OnDisable()
        {
            _manager.OnScreenShown -= OnShowScreen;
            _manager.OnScreenChanged -= OnChangeScreen;
            _manager.OnScreenHidden -= OnHideScreen;
        }

        public void ChangeScreen(T key, object args = default)
        {
            _manager.ChangeScreen(key, args);
        }

        public bool IsScreenActive(T key)
        {
            return _manager.IsScreenActive(key);
        }

        private void OnShowScreen(T screenName)
        {
            OnScreenShown?.Invoke(screenName);
        }

        private void OnHideScreen(T screenName)
        {
            OnScreenHidden?.Invoke(screenName);
        }

        private void OnChangeScreen(T screenName)
        {
            OnScreenChanged?.Invoke(screenName);
        }
    }
}