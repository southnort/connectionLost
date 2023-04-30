using Yrr.Utils;

namespace ConnectionLost.Models
{
    public sealed class HawkeyeBonus : BonusBase
    {
        public HawkeyeBonus() {
            CountOfUses = new ReactiveInt(3);
        }
    }
}
