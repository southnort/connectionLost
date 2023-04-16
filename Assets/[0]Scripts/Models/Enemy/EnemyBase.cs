using ConnectionLost.Core;
using Yrr.Core;


namespace ConnectionLost.Models
{
    public abstract class EnemyBase : ICellContent
    {
        public EnemyBase(float hp, float dmg)
        {
            Hp = new ReactiveValue<float>(hp);
            Dmg = dmg;
        }

        public virtual bool IsBlock => true;
        public virtual bool IsCanBlocked => false;
        public ReactiveValue<float> Hp { get; private set; }
        public float Dmg { get; private set; }

        public void TakeDamage(float damage)
        {
            Hp.Value -= damage;
        }
    }
}