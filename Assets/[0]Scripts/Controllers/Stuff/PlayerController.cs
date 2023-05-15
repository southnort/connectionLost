using ConnectionLost.Core;
using ConnectionLost.Models;
using ConnectionLost.Views;
using System;
using UnityEngine;
using UnityEngine.XR;
using Yrr.Utils;


namespace ConnectionLost.Controllers
{
    internal sealed class PlayerController : IDisposable
    {
        private BonusViewsBuilder _bonusViewsBuilder;
        private PlayerModel _model { get; }
        private readonly PlayerStateView _playerStateView;
        private readonly PlayerTakingBonusesView _playerTakeBonusesView;
        private int _attackDeBuff;
        private GridController _gridController;



        public bool IsAlive => _model.Hp > 0;


        public PlayerController(PlayerModel model, PlayerStateView playerStateView, PlayerTakingBonusesView playerTakeBonusesView)
        {
            _playerStateView = playerStateView;
            _model = model;
            _playerTakeBonusesView = playerTakeBonusesView;

            for (var i = 0; i < _model.TakingBonuses.Length; i++)
            {
                if (_model.TakingBonuses[i] == null)
                {
                    _playerTakeBonusesView.ClearBonus(i);
                }

                else
                {
                    if (_bonusViewsBuilder != null)
                        _playerTakeBonusesView.SetInitialized(i,
                            _bonusViewsBuilder.GetBonusIcon(_model.TakingBonuses[i]),
                            _model.TakingBonuses[i].CountOfUses.Value);
                }
            }

            _model.Hp.OnChange += OnHpChanged;
            _model.Damage.OnChange += OnDmgChanged;

            OnHpChanged(_model.Hp.Value);
            OnDmgChanged(_model.Damage.Value);
        }

        public void SetGridController(GridController gridController)
        {
            _gridController = gridController;
        }

        public void SetBonusBuilder(BonusViewsBuilder bonusViewsBuilder)
        {
            _bonusViewsBuilder = bonusViewsBuilder;
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

        public void AttackEnemy(EnemyBase enemy, HexCoordinates coords)
        {
            for (var i = 0; i < _model.TakingBonuses.Length; i++)
            {
                var bonus = _model.TakingBonuses[i];
                if (bonus is not { IsActive: true }) continue;
                switch (bonus)
                {
                    case HawkEyeBonus eye:
                        bonus.IsActive = false;

                        _gridController.OnEndOfPlayerTurn +=
                            () => eye.AttackEnemy(enemy);

                        eye.CountOfUses.OnChange += (int countOfUse) =>
                        {
                            if (countOfUse <= 0)
                            {
                                _gridController.OnEndOfPlayerTurn -=
                                    () => eye.AttackEnemy(enemy);
                                int index = i * 1;
                                RemoveBonus(index);
                            }
                        };
                        return;

                    case RepairBonus repair:


                    case HalfHpBonus:
                        RemoveBonus(i);
                        enemy.TakeDamage(enemy.Hp.Value / 2f);
                        return;
                }
            }

            enemy.TakeDamage(_model.Damage.Value);


            if (!(enemy.Hp.Value > 0)) return;
            {
                for (var i = 0; i < _model.TakingBonuses.Length; i++)
                {
                    var bonus = _model.TakingBonuses[i];
                    if (bonus is not ShieldBonus || !bonus.IsActive) continue;
                    bonus.CountOfUses--;

                    if (bonus.CountOfUses <= 0)
                    {
                        RemoveBonus(i);
                    }

                    return;
                }

                _model.Hp -= enemy.Dmg;
            }
        }


        public bool TryTakeBonus(BonusBase bonus)
        {
            for (var i = 0; i < _model.TakingBonuses.Length; i++)
            {
                if (_model.TakingBonuses[i] != null) continue;
                _model.TakingBonuses[i] = bonus;
                bonus.IndexInPlayerBonuses = i;
                _playerTakeBonusesView.SetInitialized(i, _bonusViewsBuilder.GetBonusIcon(bonus), bonus.CountOfUses.Value);
                bonus.CountOfUses.OnChange += x =>
                {
                    _playerTakeBonusesView.UpdateCount(i * 1, x);
                };
                return true;
            }

            return false;
        }

        public void RemoveBonus(int index)
        {
            _model.TakingBonuses[index] = null;
            _playerTakeBonusesView.ClearBonus(index);
        }

        public void AddAttackDeBuff()
        {
            _attackDeBuff++;
            UpdateAttack();
        }

        public void RemoveAttackDeBuff()
        {
            _attackDeBuff--;
            if (_attackDeBuff < 0)
                _attackDeBuff = 0;
            UpdateAttack();
        }

        private void UpdateAttack()
        {
            var attackModification = _attackDeBuff * GameConfig.SuppressorDeBuffValuePerLevel;

            _model.Damage -= attackModification;

            if (_model.Damage < GameConfig.PlayerMinPower)
            {
                _model.Damage.Value = GameConfig.PlayerMinPower;
            }
        }


        internal void ActivateBonus(int index)
        {
            if (_model.TakingBonuses[index] == null || _model.TakingBonuses[index].IsActive)
                return;

            var bonus = _model.TakingBonuses[index];

            bonus.IsActive = true;

            if (bonus is RepairBonus repair)
            {
                repair.RepairPlayer(_model);
                _gridController.OnEndOfPlayerTurn +=
                    () => repair.RepairPlayer(_model);

                repair.CountOfUses.OnChange += (int countOfUse) =>
                {
                    if (countOfUse <= 0)
                    {
                        _gridController.OnEndOfPlayerTurn -=
                            () => repair.RepairPlayer(_model);
                        RemoveBonus(index);
                    }
                };
            }

            if (bonus.CountOfUses <= 0)
            {
                RemoveBonus(index);
            }

            else
            {
                _playerTakeBonusesView.UpdateCount(index, bonus.CountOfUses.Value);
            }

        }



        public void Dispose()
        {
            _model.Hp.OnChange -= OnHpChanged;
            _model.Damage.OnChange -= OnDmgChanged;
        }


    }
}
