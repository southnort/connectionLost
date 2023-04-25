using ConnectionLost.Core;

namespace ConnectionLost.Models
{
    public abstract class BonusBase : ICellContent
    {
        public bool IsBlock => false;

        public bool IsCanBlocked => true;
    }
}
