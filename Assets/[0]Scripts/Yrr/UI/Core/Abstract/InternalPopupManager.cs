using System;
using System.Collections.Generic;


namespace Yrr.UI.Core
{
    internal sealed class InternalPopupManager<TKey, TValue> : IPopupManager<TKey>, IScreen.ICallback where TValue : IScreen
    {
        public event Action<TKey> OnPopupShown;
        public event Action<TKey> OnPopupHidden;

        private readonly IScreenSupplier<TKey, TValue> _supplier;
        private readonly Dictionary<TKey, TValue> _activePopups;
        private readonly List<TKey> _cache;

        public InternalPopupManager(IScreenSupplier<TKey, TValue> supplier)
        {
            _supplier = supplier;
            _activePopups = new Dictionary<TKey, TValue>();
            _cache = new List<TKey>();
        }

        public void ShowPopup(TKey key, object args = default)
        {
            if (!IsPopupActive(key))
            {
                ShowPopupInternal(key, args);
            }
        }

        public bool IsPopupActive(TKey key)
        {
            return _activePopups.ContainsKey(key);
        }

        public void HidePopup(TKey key)
        {
            if (IsPopupActive(key))
            {
                HidePopupInternal(key);
            }
        }

        public void HideAllPopups()
        {
            _cache.Clear();
            _cache.AddRange(_activePopups.Keys);

            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                var popupName = _cache[i];
                HidePopupInternal(popupName);
            }
        }

        void IScreen.ICallback.OnClose(IScreen frame)
        {
            var popup = (TValue)frame;
            if (TryFindScreen(popup, out var popupName))
            {
                HidePopup(popupName);
            }
        }

        private void ShowPopupInternal(TKey name, object args)
        {
            var popup = _supplier.LoadScreen(name);
            popup.Show(args, callback: this);

            _activePopups.Add(name, popup);
            OnPopupShown?.Invoke(name);
        }

        private void HidePopupInternal(TKey name)
        {
            var popup = _activePopups[name];
            popup.Hide();

            _activePopups.Remove(name);
            _supplier.UnloadScreen(popup);
            OnPopupHidden?.Invoke(name);
        }

        private bool TryFindScreen(TValue popup, out TKey screen)
        {
            foreach (var (key, otherPopup) in _activePopups)
            {
                if (ReferenceEquals(popup, otherPopup))
                {
                    screen = key;
                    return true;
                }
            }

            screen = default;
            return false;
        }
    }
}