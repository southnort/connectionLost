using ConnectionLost.Core;

namespace ConnectionLost.Models.Enemy
{
    public sealed class CoreModelFactory : EnemyAbstractFactory
    {
        public override EnemyBase CreateEnemy(GridDifficult difficult)
        {
            var core = new CoreModel
            {
                CoreDifficult = difficult,
                Dmg = 10
            };

            core.Hp = difficult switch
            {
                GridDifficult.Tutorial => 50,
                GridDifficult.Easy => 70,
                GridDifficult.Medium => 70,
                GridDifficult.Hard => 90,
                _ => core.Hp
            };

            return core;
        }
    }
}
