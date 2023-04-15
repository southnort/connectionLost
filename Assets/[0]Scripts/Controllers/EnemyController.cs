using ConnectionLost.Models;
using ConnectionLost.Views;
using System;
using Yrr.Core;

namespace ConnectionLost.Controllers
{
    public sealed class EnemyController : IDisposable
    {
        private readonly EnemyView _view;
        private readonly EnemyBase _model;


        public EnemyController(EnemyView view, EnemyBase model)
        {
            _view = view;
            _model = model;

            _view.SetInitialData(_model.Hp.ToIntString(), _model.Dmg.ToIntString());
            _model.OnHealthChanged += OnHpChanged;
        }

        private void OnHpChanged(float hp)
        {
            var str = hp.ToIntString();
            _view.UpdateHp(str);
        }

        public void Dispose()
        {
            _model.OnHealthChanged -= OnHpChanged;
        }
    }
}
