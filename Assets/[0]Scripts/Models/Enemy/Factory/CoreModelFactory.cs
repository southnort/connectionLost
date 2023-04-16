using ConnectionLost.Core;

namespace ConnectionLost.Models.Enemy
{
    public sealed class CoreModelFactory : EnemyAbstractFactory
    {
        public override EnemyBase CreateEnemy(GridDifficult difficult)
        {
            float hp = difficult switch
            {
                GridDifficult.Tutorial => 50,
                GridDifficult.Easy => 70,
                GridDifficult.Medium => 70,
                GridDifficult.Hard => 90,
                _ => 0,
            };

            var core = new CoreModel(hp, 10, difficult);
            return core;
        }
    }
}
