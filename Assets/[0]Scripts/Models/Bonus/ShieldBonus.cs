using Yrr.Utils;


namespace ConnectionLost.Models
{
    public sealed class ShieldBonus : BonusBase
    {
        public ShieldBonus()
        {
            CountOfUses = new ReactiveInt(2);
        }

    }
}
