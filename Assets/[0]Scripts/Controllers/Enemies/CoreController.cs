using ConnectionLost.Models;
using ConnectionLost.Models.Enemy;
using ConnectionLost.Views;

namespace ConnectionLost.Controllers
{
    internal sealed class CoreController : EnemyController
    {
        private CoreModel _coreModel;

        public CoreController(EnemyView view, CoreModel model) : base(view, model)
        {
            _coreModel = model;
        }

        internal override void SetGridController(GridController gridController)
        {
            base.SetGridController(gridController);

            OnDeath +=
                () => GridController.OnCoreDeath();
        }
    }
}
