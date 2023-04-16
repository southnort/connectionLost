using ConnectionLost.Models;
using ConnectionLost.Views;
using Yrr.Core;


namespace ConnectionLost.Controllers
{
    public sealed class EnemyController : IContentController
    {
        private readonly EnemyView _view;
        private readonly EnemyBase _model;


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
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(_view.gameObject);
            _model.Hp.OnChange -= OnHpChanged;
        }
    }
}
