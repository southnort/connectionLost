using System;
using UnityEngine;
using Yrr.UI.Core;


namespace Yrr.UI
{
    public sealed class PopupManager : AbstractPopupManager<Type>
    {
        [SerializeField] private MyPopupSupplier supplier;

        protected override IScreenSupplier<Type, UIScreen> Supplier => supplier;
    }
}