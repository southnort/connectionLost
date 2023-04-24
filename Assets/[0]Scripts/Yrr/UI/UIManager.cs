using UnityEngine;
using System;


namespace Yrr.UI
{
    public sealed class UIManager : MonoBehaviour
    {
        [SerializeField] private ScreenManager screenManager;
        [SerializeField] private PopupManager popupManager;
        [SerializeField] private MyScreenStorage screenStorage;

        private void Start()
        {
            var windows = transform.GetComponentsInChildren<UIScreen>(true);

            foreach (var screen in windows)
            {
                screenStorage.AddScreen(screen.GetType(), screen);
                screen.Hide();
            }

            GoToWindow(windows[0]);
        }

        public void GoToWindow(UIScreen screen)
        {
            screenManager.ChangeScreen(screen.GetType());
        }

        public void GoToWindow(Type key, object args = null)
        {
            screenManager.ChangeScreen(key, args);
        }

        public void GoToWindow<T>(object args = null) where T : UIScreen
        {
            GoToWindow(typeof(T), args);
        }

        public void ShowPopup(Type key, object args = null)
        {
            popupManager.ShowPopup(key, args);
        }
    }
}