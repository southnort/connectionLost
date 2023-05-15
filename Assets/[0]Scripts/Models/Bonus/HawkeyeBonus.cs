using ConnectionLost.Core;
using Yrr.Utils;


namespace ConnectionLost.Models
{
    public sealed class HawkEyeBonus : BonusBase
    {
        private float DamagePerTick => 20f;

        public HawkEyeBonus()
        {
            CountOfUses = new ReactiveInt(3);
        }


        public void AttackEnemy(EnemyBase enemy)
        {
            if (enemy == null)
            {
                CountOfUses.Value = 0;
                return;
            }

            CountOfUses--;
            enemy.TakeDamage(DamagePerTick);



        }
    }
}
