using UnityEngine;
using Yrr.Utils;


namespace ConnectionLost.Models
{
    public sealed class RepairBonus : BonusBase
    {
        public RepairBonus(float minHealValue, float maxHealValue)
        {
            CountOfUses = new ReactiveInt(3);

            _minHealValue = minHealValue;
            _maxHealValue = maxHealValue;
        }

        private float _minHealValue;
        private float _maxHealValue;


        public void RepairPlayer(PlayerModel player)
        {
            CountOfUses--;
            player.Hp+= Mathf.Round(Random.Range(_minHealValue, _maxHealValue));
        }
    }
}
