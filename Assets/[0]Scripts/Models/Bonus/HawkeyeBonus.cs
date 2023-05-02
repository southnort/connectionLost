using ConnectionLost.Core;
using Yrr.Utils;


namespace ConnectionLost.Models
{
    public sealed class HawkEyeBonus : BonusBase
    {
        public float DamagePerTick => 20f;
        public HexCoordinates AttackedEnemyCoordinates { get; set; }

        public HawkEyeBonus()
        {
            CountOfUses = new ReactiveInt(3);
        }
    }
}
