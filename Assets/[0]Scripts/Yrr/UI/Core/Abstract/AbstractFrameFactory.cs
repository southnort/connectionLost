using UnityEngine;


namespace Yrr.UI.Core
{
    internal abstract class AbstractFrameFactory<TKey, TValue> : MonoBehaviour, IScreenFactory<TKey, TValue> where TValue : UIScreen
    {
        [SerializeField]
        private Transform container;

        public TValue CreateScreen(TKey key)
        {
            var prefab = GetPrefab(key);
            var popup = Instantiate(prefab, container);
            OnFrameCreated(popup);
            return popup;
        }

        protected abstract TValue GetPrefab(TKey key);

        protected virtual void OnFrameCreated(TValue popup)
        {
        }
    }
}