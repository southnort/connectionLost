using ConnectionLost.Core;

namespace ConnectionLost.Models.Enemy
{
    public sealed class FirewallModelFactory : EnemyAbstractFactory
    {
        public override EnemyBase CreateEnemy(GridDifficult difficult)
        {
            var enemy = new FirewallModel
            {
                Dmg = 20
            };

            enemy.Hp = difficult switch
            {
                GridDifficult.Tutorial => 40,
                GridDifficult.Easy => 60,
                GridDifficult.Medium => 80,
                GridDifficult.Hard => 90,
                _ => enemy.Hp
            };

            return enemy;
        }
    }
}
