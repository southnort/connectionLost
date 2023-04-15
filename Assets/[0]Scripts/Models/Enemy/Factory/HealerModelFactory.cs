using ConnectionLost.Core;


namespace ConnectionLost.Models.Enemy
{
    public sealed class HealerModelFactory : EnemyAbstractFactory
    {
        public override EnemyBase CreateEnemy(GridDifficult difficult)
        {
            var enemy = new HealerModel
            {
                Hp = 80,
                Dmg = 10
            };

            return enemy;
        }
    }
}
