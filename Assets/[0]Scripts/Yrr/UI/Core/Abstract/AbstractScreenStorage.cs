using System.Collections.Generic;
using UnityEngine;


namespace Yrr.UI.Core
{
    public abstract class AbstractScreenStorage<TKey, TValue> : MonoBehaviour, IScreenSupplier<TKey, TValue> where TValue : UIScreen
    {
        private readonly Dictionary<TKey, TValue> _cachedScreens;

        protected AbstractScreenStorage()
        {
            _cachedScreens = new Dictionary<TKey, TValue>();
        }

        public void AddScreen(TKey key, TValue value)
        {
            _cachedScreens.Add(key, value);
        }

        public TValue LoadScreen(TKey key)
        {
            var screen = _cachedScreens[key];
            return screen;
        }       
    }
}
