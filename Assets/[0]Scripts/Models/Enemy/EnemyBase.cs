using ConnectionLost.Core;
using Yrr.Utils;


namespace ConnectionLost.Models
{
    public abstract class EnemyBase : ICellContent
    {
        protected EnemyBase(float hp, float dmg)
        {
            Hp = new ReactiveFloat(hp);
            Dmg = dmg;
        }

        public virtual bool IsBlock => true;
        public virtual bool IsCanBlocked => false;
        public ReactiveFloat Hp { get; private set; }
        public float Dmg { get; }

        public void TakeDamage(float damage)
        {
            Hp -= damage;
        }

        public void Heal(float heal)
        {
            Hp += heal;
        }
    }
}