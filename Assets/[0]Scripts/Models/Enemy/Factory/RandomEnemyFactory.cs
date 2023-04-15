using ConnectionLost.Core;
using Yrr.Core;

namespace ConnectionLost.Models.Enemy
{
    public sealed class RandomEnemyFactory : EnemyAbstractFactory
    {
        private readonly RandomizerByWeight<EnemyAbstractFactory> _randomizer = new();

        public override EnemyBase CreateEnemy(GridDifficult difficult)
        {
            if (_randomizer.Count == 0)
                InitializeRandomizer(difficult);

            var factory = _randomizer.GetRandom();
            return factory.CreateEnemy(difficult);
        }

        private void InitializeRandomizer(GridDifficult difficult)
        {
            _randomizer.AddVariant(new FirewallModelFactory(), GameConfig.FirewallSpawnWeight);
            _randomizer.AddVariant(new AntivirusModelFactory(), GameConfig.AntivirusSpawnWeight);

            if (((int)difficult) > 1)
                _randomizer.AddVariant(new HealerModelFactory(), GameConfig.HealerSpawnWeight);

            if ((int)difficult > 2)
                _randomizer.AddVariant(new SuppressorModelFactory(), GameConfig.SuppressorSpawnWeight);
        }
    }
}
