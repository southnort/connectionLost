using ConnectionLost.Core;

namespace ConnectionLost.Models.Enemy
{
    public sealed class AntivirusModelFactory : EnemyAbstractFactory
    {
        public override EnemyBase CreateEnemy(GridDifficult difficult)
        {
            float hp = 0;
            float dmg = 0;

            switch (difficult)
            {
                case GridDifficult.Tutorial:
                    dmg = 30;
                    hp = 30; break;

                case GridDifficult.Easy:
                    dmg = 40;
                    hp = 30; break;

                case GridDifficult.Medium:
                    dmg = 40;
                    hp = 50; break;

                case GridDifficult.Hard:
                    dmg = 40;
                    hp = 60; break;
            }

            var enemy = new AntivirusModel(hp, dmg);
            return enemy;
        }

    }
}
