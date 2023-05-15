using ConnectionLost.Models;
using ConnectionLost.Views;
using System;
using Yrr.Utils;


namespace ConnectionLost.Controllers
{
    internal class EnemyController : IContentController
    {
        private readonly EnemyView _view;
        private readonly EnemyBase _model;

        protected GridController GridController;
        protected event Action OnDeath;


        public EnemyController(EnemyView view, EnemyBase model)
        {
            _view = view;
            _model = model;

            _view.SetInitialData(_model.Hp.Value.ToIntString(), _model.Dmg.ToIntString());
            _model.Hp.OnChange += OnHpChanged;
        }

        private void OnHpChanged(float hp)
        {
            var str = hp.ToIntString();
            _view.UpdateHp(str);

            if (hp <= 0)
                OnDeath?.Invoke();
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(_view.gameObject);
            _model.Hp.OnChange -= OnHpChanged;
        }

        public void Heal(float healValue)
        {
            _model.Heal(healValue);
        }

        internal virtual void SetGridController(GridController gridController)
        {
            GridController = gridController;
        }
    }
}
