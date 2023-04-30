using ConnectionLost.Core;
using Yrr.Utils;

namespace ConnectionLost.Models
{
    public sealed class RandomBonusFactory
    {
        private delegate BonusBase CreateBonusDelegate();
        private readonly RandomizerByWeight<CreateBonusDelegate> _randomizer = new RandomizerByWeight<CreateBonusDelegate>();


        public BonusBase CreateBonus(GridDifficult difficult)
        {
            if (_randomizer.Count == 0)
                InitializeRandomizer(difficult);

            return _randomizer.GetRandom().Invoke();
        }

        private void InitializeRandomizer(GridDifficult difficult)
        {
            _randomizer.AddVariant(() => new RepairBonus(10, 25), GameConfig.RepairBonusSpawnWeight);
            _randomizer.AddVariant(() => new HalfHpBonus(), GameConfig.HalfHpBonusSpawnWeight);

            if ((int)difficult > 1)
            {
                _randomizer.AddVariant(() => new ShieldBonus(), GameConfig.ShieldBonusSpawnWeight);
                _randomizer.AddVariant(() => new HawkeyeBonus(), GameConfig.HawkeyeBonusSpawnWeight);
            }
        }
    }
}
