using ConnectionLost.Core;

namespace ConnectionLost.Models.Enemy
{
    public sealed class FirewallModelFactory : EnemyAbstractFactory
    {
        public override EnemyBase CreateEnemy(GridDifficult difficult)
        {
            float hp = difficult switch
            {
                GridDifficult.Tutorial => 40,
                GridDifficult.Easy => 60,
                GridDifficult.Medium => 80,
                GridDifficult.Hard => 90,
                _ => 0,
            };

            var enemy = new FirewallModel(hp, 20);
            return enemy;
        }
    }
}
