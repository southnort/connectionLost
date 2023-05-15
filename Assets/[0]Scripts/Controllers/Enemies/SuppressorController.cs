using ConnectionLost.Models;
using ConnectionLost.Views;

namespace ConnectionLost.Controllers
{
    internal sealed class SuppressorController : EnemyController
    {
        public SuppressorController(EnemyView view, EnemyBase model) : base(view, model) { }

        internal override void SetGridController(GridController gridController)
        {
            base.SetGridController(gridController);

            GridController.GetPlayer().AddAttackDeBuff();

            OnDeath += ()
                => GridController.GetPlayer().RemoveAttackDeBuff();
        }
    }
}
