using ConnectionLost.Core;
using ConnectionLost.Models;
using ConnectionLost.Views;
using System;
using Yrr.Utils;

namespace ConnectionLost.Controllers
{
    internal sealed class PlayerController : IDisposable
    {
        private readonly PlayerModel _model;
        private readonly PlayerStateView _playerStateView;
        private int _attackDebuff;


        public bool IsAlive => _model.Hp > 0;




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
                _model.Hp -= enemy.Dmg;
            }
        }

        public bool TryTakeBonus(BonusBase bonus)
        {
            for (int i = 0; i < _model.TakedBonuses.Length; i++)
            {
                if (_model.TakedBonuses[i] == null)
                {
                    _model.TakedBonuses[i] = bonus;
                    return true;
                }
            }

            return false;
        }



        public void AddAttackDebuff()
        {
            _attackDebuff++;
            UpdateAttack();
        }

        public void RemoveAttackDebuff()
        {
            _attackDebuff--;
            if (_attackDebuff < 0)
                _attackDebuff = 0;
            UpdateAttack();
        }

        private void UpdateAttack()
        {
            var attackModification = _attackDebuff * GameConfig.SuppressorDebuffValuePerLevel;

            _model.Damage -= attackModification;

            if (_model.Damage < GameConfig.PlayerMinStrenght)
            {
                _model.Damage.Value = GameConfig.PlayerMinStrenght;
            }
        }




        public void Dispose()
        {
            _model.Hp.OnChange -= OnHpChanged;
            _model.Damage.OnChange -= OnDmgChanged;
        }


    }
}
