using ConnectionLost.Core;
using Yrr.Utils;

namespace ConnectionLost.Models
{
    public abstract class BonusBase : ICellContent
    {
        public bool IsBlock => false;

        public bool IsCanBlocked => true;

        public ReactiveInt CountOfUses = new ReactiveInt(1);

        public bool IsActive { get; set; }
    }
}
