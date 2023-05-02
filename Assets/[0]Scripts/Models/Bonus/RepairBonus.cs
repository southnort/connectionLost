using Yrr.Utils;


namespace ConnectionLost.Models
{
    public sealed class RepairBonus : BonusBase
    {
        public RepairBonus(float minHealValue, float maxHealValue)
        {
            CountOfUses = new ReactiveInt(3);

            MinHealValue = minHealValue;
            MaxHealValue = maxHealValue;
        }

        public float MinHealValue { get; }
        public float MaxHealValue { get; }
    }
}
