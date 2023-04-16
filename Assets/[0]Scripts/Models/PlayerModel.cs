using Yrr.Core;


namespace ConnectionLost.Models
{
    public sealed class PlayerModel
    {
        public ReactiveValue<float> Hp { get; private set; } = new ReactiveValue<float>();

        public ReactiveValue<float> Damage { get; private set; } = new ReactiveValue<float>();
    }
}
