using ConnectionLost.Core;
using Yrr.Utils;


namespace ConnectionLost.Models
{
    public sealed class PlayerModel
    {
        public ReactiveFloat Hp { get; set; }
        public ReactiveFloat Damage { get; set; }

        public PlayerModel(PlayerData playerData)
        {
            Hp = new ReactiveFloat(playerData.BaseHp);
            Damage = new ReactiveFloat(playerData.BaseDmg);
        }
    }
}
