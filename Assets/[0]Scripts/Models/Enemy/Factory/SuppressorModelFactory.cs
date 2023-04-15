using ConnectionLost.Core;


namespace ConnectionLost.Models.Enemy
{
    public sealed class SuppressorModelFactory : EnemyAbstractFactory
    {
        public override EnemyBase CreateEnemy(GridDifficult difficult)
        {
            var enemy = new SuppressorModel
            {
                Dmg = 15,
                Hp = 60
            };

            return enemy;
        }
    }
}
