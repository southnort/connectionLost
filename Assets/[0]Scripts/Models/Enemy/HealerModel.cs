

namespace ConnectionLost.Models
{
    public sealed class HealerModel : EnemyBase
    {
        public HealerModel(float hp, float dmg) : base(hp, dmg) { }

        public float HealValue => 20f;
    }
}
