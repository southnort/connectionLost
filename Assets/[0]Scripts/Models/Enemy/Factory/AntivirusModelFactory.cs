using ConnectionLost.Core;

namespace ConnectionLost.Models.Enemy
{
    public sealed class AntivirusModelFactory : EnemyAbstractFactory
    {
        public override EnemyBase CreateEnemy(GridDifficult difficult)
        {
            var enemy = new AntivirusModel();

            switch (difficult)
            {
                case GridDifficult.Tutorial:
                    enemy.Dmg = 30;
                    enemy.Hp = 30; break;

                case GridDifficult.Easy:
                    enemy.Dmg = 40;
                    enemy.Hp = 30; break;

                case GridDifficult.Medium:
                    enemy.Dmg = 40;
                    enemy.Hp = 50; break;

                case GridDifficult.Hard:
                    enemy.Dmg = 40;
                    enemy.Hp = 60; break;
            }

            return enemy;
        }

    }
}
