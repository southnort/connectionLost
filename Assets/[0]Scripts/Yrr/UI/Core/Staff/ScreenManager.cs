using UnityEngine;
using Yrr.UI.Core;
using System;


namespace Yrr.UI
{
    public sealed class ScreenManager : AbstractScreenManager<Type>
    {
        [SerializeField] private MyScreenStorage supplier;

        protected override IScreenSupplier<Type, UIScreen> Supplier => supplier;
    }
}