using UnityEngine;
using System;


namespace Yrr.UI.Core
{
    internal sealed class MyScreenSupplier : AbstractFrameSupplier<Type, UIScreen>
    {
        [SerializeField] private MyScreenFactory factory;

        protected override UIScreen InstantiateFrame(Type key)
        {
            return factory.CreateScreen(key);
        }
    }
}