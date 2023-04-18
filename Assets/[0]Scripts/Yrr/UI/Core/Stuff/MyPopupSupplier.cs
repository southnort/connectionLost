using System;
using UnityEngine;


namespace Yrr.UI.Core
{
    internal sealed class MyPopupSupplier : AbstractFrameSupplier<Type, UIScreen>
    {
        [SerializeField] private MyPopupFactory factory;

        protected override UIScreen InstantiateFrame(Type key)
        {
            return factory.CreateScreen(key);
        }
    }
}