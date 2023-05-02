

namespace ConnectionLost.Models
{
    public sealed class SuppressorModel : EnemyBase
    {
        public SuppressorModel(float hp, float dmg) : base(hp, dmg) { }

        public int DeBuffPower => 1;
    }
}
