using ConnectionLost.Core;


namespace ConnectionLost.Models.Enemy
{
    public sealed class SuppressorModelFactory : EnemyAbstractFactory
    {
        public override EnemyBase CreateEnemy(GridDifficult difficult)
        {
            var enemy = new SuppressorModel(60, 15);

            return enemy;
        }
    }
}
