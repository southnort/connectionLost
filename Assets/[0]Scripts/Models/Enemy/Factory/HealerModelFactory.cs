using ConnectionLost.Core;


namespace ConnectionLost.Models.Enemy
{
    public sealed class HealerModelFactory : EnemyAbstractFactory
    {
        public override EnemyBase CreateEnemy(GridDifficult difficult)
        {
            var enemy = new HealerModel(80, 10);
            return enemy;
        }
    }
}
