using ConnectionLost.Core;


namespace ConnectionLost.Controllers
{
    internal sealed class ClickResult
    {
        public bool NeedUpdate { get; internal set; }
        public ICellContent CellContent { get; internal set; }
    }
}
