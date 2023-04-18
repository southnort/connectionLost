using ConnectionLost.Core;
using Yrr.Utils;


namespace ConnectionLost.Models
{
    public abstract class EnemyBase : ICellContent
    {
        protected EnemyBase(float hp, float dmg)
        {
            Hp = new ReactiveValue<float>(hp);
            Dmg = dmg;
        }

        public virtual bool IsBlock => true;
        public virtual bool IsCanBlocked => false;
        public ReactiveValue<float> Hp { get; }
        public float Dmg { get; }

        public void TakeDamage(float damage)
        {
            Hp.Value -= damage;
        }
    }
}