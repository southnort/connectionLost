using ConnectionLost.Models;
using ConnectionLost.Models.Enemy;
using ConnectionLost.Views;

namespace ConnectionLost.Controllers
{
    internal sealed class EnemyControllerFactory
    {
        internal EnemyController CreateEnemyController(EnemyView view, EnemyBase enemy)
        {
            EnemyController result;

            if (enemy is HealerModel hm)
                result = new HealerController(view, hm);

            else if(enemy is CoreModel cm)
                result = new CoreController(view, cm);

            else if (enemy is SuppressorModel sm)
                result = new SuppressorController(view, sm);

            else result = new EnemyController(view, enemy);


            return result;
        }
    }
}
