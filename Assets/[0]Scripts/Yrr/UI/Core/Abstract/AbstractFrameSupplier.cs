using System.Collections.Generic;
using UnityEngine;


namespace Yrr.UI.Core
{
    public abstract class AbstractFrameSupplier<TKey, TValue> : MonoBehaviour, IScreenSupplier<TKey, TValue> where TValue : UIScreen
    {
        private readonly Dictionary<TKey, TValue> _cachedScreens;

        protected AbstractFrameSupplier()
        {
            _cachedScreens = new Dictionary<TKey, TValue>();
        }

        public TValue LoadScreen(TKey key)
        {
            if (_cachedScreens.TryGetValue(key, out var frame))
            {
                frame.gameObject.SetActive(true);
            }
            else
            {
                frame = InstantiateFrame(key);
                _cachedScreens.Add(key, frame);
            }

            frame.transform.SetAsLastSibling();
            return frame;
        }

        public void UnloadScreen(TValue frame)
        {
            frame.gameObject.SetActive(false);
        }

        protected abstract TValue InstantiateFrame(TKey key);
    }
}