using UnityEngine;
using Yrr.UI.Core;
using System;


namespace Yrr.UI
{
    public sealed class ScreenManager : AbstractScreenManager<Type>
    {
        [SerializeField] private MyScreenSupplier supplier;

        protected override IScreenSupplier<Type, UIScreen> Supplier => supplier;
    }
}