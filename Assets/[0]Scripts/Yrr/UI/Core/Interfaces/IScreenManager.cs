using System;


namespace Yrr.UI.Core
{
    internal interface IScreenManager<TKey>
    {
        event Action<TKey> OnScreenShown;
        event Action<TKey> OnScreenHidden;
        event Action<TKey> OnScreenChanged;


        void ChangeScreen(TKey key, object args = default);

        bool IsScreenActive(TKey key);
    }
}