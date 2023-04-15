using ConnectionLost.Core;
using System.Collections.Generic;


namespace ConnectionLost.Controllers
{
    internal sealed class ClickResult
    {
        public bool NeedUpdate { get; internal set; }
        public ICellContent CellContent { get; internal set; }
    }
}
