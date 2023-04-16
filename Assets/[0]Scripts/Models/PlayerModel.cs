using ConnectionLost.Core;
using Yrr.Core;


namespace ConnectionLost.Models
{
    public sealed class PlayerModel
    {
        public ReactiveValue<float> Hp { get; private set; }
        public ReactiveValue<float> Damage { get; private set; }

        public PlayerModel(PlayerData playerData)
        {
            Hp = new ReactiveValue<float>(playerData.BaseHp);
            Damage = new ReactiveValue<float>(playerData.BaseDmg);
        }
    }
}
