using System;
using UnityEngine;


namespace Yrr.UI.Core
{
    internal sealed class MyPopupFactory : AbstractFrameFactory<Type, UIScreen>
    {
        [SerializeField] private UIScreen[] popups;

        protected override UIScreen GetPrefab(Type key)
        {
            foreach (var item in popups)
            {
                if (item.GetType() == key)
                {
                    return item;
                }
            }

            throw new Exception($"Screen {key} is not found!");
        }
    }
}