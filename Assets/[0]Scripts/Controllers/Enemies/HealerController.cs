using ConnectionLost.Controllers;
using ConnectionLost.Models;
using ConnectionLost.Views;
using System.Linq;
using Yrr.Utils;

namespace ConnectionLost.Controllers
{
    internal sealed class HealerController : EnemyController
    {
        private HealerModel _model;

        internal HealerController(EnemyView view, HealerModel model) : base(view, model)
        {
            _model = model;
        }

        internal override void SetGridController(GridController gridController)
        {
            base.SetGridController(gridController);
            GridController.OnEndOfPlayerTurn += Heal;
            OnDeath +=
                () => GridController.OnEndOfPlayerTurn -= Heal;
        }

        private void Heal()
        {
            var allEnemies = GridController.GetActiveEnemies();
            if (allEnemies.Any())
            {
                var enemy = allEnemies.Where(x => x is not HealerController).GetRandomItem();
                if (enemy == null)
                    enemy = allEnemies.GetRandomItem();

                if (enemy != null)
                {
                    enemy.Heal(_model.HealValue);
                }
            }
        }
    }
}
