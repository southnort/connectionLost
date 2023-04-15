using ConnectionLost.Core;

namespace ConnectionLost.Models.Enemy
{
    public abstract class EnemyAbstractFactory
    {
        public abstract EnemyBase CreateEnemy(GridDifficult difficult);
    }
}
