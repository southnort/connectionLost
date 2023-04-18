using System;


namespace Yrr.UI.Core
{
    internal sealed class InternalScreenManager<TKey, TValue> : IScreenManager<TKey> where TValue : IScreen
    {
        public event Action<TKey> OnScreenShown;
        public event Action<TKey> OnScreenHidden;
        public event Action<TKey> OnScreenChanged;

        private readonly IScreenSupplier<TKey, TValue> _supplier;

        private TKey _currentScreenKey;
        private TValue _currentScreen;


        public InternalScreenManager(IScreenSupplier<TKey, TValue> supplier)
        {
            _supplier = supplier;
        }

        public bool IsScreenActive(TKey key)
        {
            return ReferenceEquals(_currentScreenKey, key);
        }

        public void ChangeScreen(TKey key, object args = default)
        {
            if (!ReferenceEquals(_currentScreen, null))
            {
                HideScreenInternal(_currentScreenKey, _currentScreen);
            }

            _currentScreenKey = key;
            ShowScreenInternal(key, args);
            OnScreenChanged?.Invoke(key);
        }

        private void ShowScreenInternal(TKey key, object args)
        {
            _currentScreen = _supplier.LoadScreen(key);
            _currentScreen.Show(args);
            OnScreenShown?.Invoke(key);
        }

        private void HideScreenInternal(TKey key, TValue screen)
        {
            screen.Hide();
            _supplier.UnloadScreen(screen);
            OnScreenHidden?.Invoke(key);
        }
    }
}