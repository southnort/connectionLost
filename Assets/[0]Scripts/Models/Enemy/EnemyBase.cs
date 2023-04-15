using ConnectionLost.Core;
using System;


namespace ConnectionLost.Models
{
    public abstract class EnemyBase : ICellContent
    {
        public event Action<float> OnHealthChanged;

        public virtual bool IsBlock => true;
        public virtual bool IsCanBlocked => false;

        public float Hp { get; internal set; }
        public float Dmg { get; internal set; }

        public void TakeDamage(float damage)
        {
            Dmg -= damage;
            OnHealthChanged?.Invoke(Dmg);
        }
    }
}