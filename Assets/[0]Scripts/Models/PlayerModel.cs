using ConnectionLost.Core;
using Yrr.Utils;


namespace ConnectionLost.Models
{
    public sealed class PlayerModel
    {
        public ReactiveValue<float> Hp { get; }
        public ReactiveValue<float> Damage { get; }

        public PlayerModel(PlayerData playerData)
        {
            Hp = new ReactiveValue<float>(playerData.BaseHp);
            Damage = new ReactiveValue<float>(playerData.BaseDmg);
        }
    }
}
