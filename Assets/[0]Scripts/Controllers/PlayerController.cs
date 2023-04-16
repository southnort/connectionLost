using ConnectionLost.Models;
using ConnectionLost.Views;
using System;
using Yrr.Core;

namespace ConnectionLost.Controllers
{
    internal sealed class PlayerController : IDisposable
    {
        private readonly PlayerModel _model;
        private PlayerStateView _playerStateView;

        public bool IsAlive => _model.Hp.Value > 0;

        public PlayerController(PlayerModel model, PlayerStateView playerStateView)
        {
            _playerStateView = playerStateView;
            _model = model;

            _model.Hp.OnChange += OnHpChanged;
            _model.Damage.OnChange += OnDmgChanged;

            OnHpChanged(_model.Hp.Value);
            OnDmgChanged(_model.Damage.Value);
        }

        private void OnHpChanged(float hp)
        {
            if (_playerStateView)
                _playerStateView.UpdateHp(hp.ToIntString());
        }

        private void OnDmgChanged(float dmg)
        {
            if (_playerStateView)
                _playerStateView.UpdateDmg(dmg.ToIntString());
        }

        public void AttackEnemy(EnemyBase enemy)
        {
            enemy.TakeDamage(_model.Damage.Value);

            if (enemy.Hp.Value > 0)
            {
                _model.Hp.Value -= enemy.Dmg;
            }
        }


        public void Dispose()
        {
            _model.Hp.OnChange -= OnHpChanged;
            _model.Damage.OnChange -= OnDmgChanged;
        }
    }
}
