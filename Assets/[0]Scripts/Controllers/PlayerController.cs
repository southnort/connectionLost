using ConnectionLost.Core;
using ConnectionLost.Models;
using ConnectionLost.Views;
using System;
using UnityEngine;
using Yrr.Utils;


namespace ConnectionLost.Controllers
{
    internal sealed class PlayerController : IDisposable
    {
        private BonusViewsBuilder _bonusViewsBuilder;
        internal PlayerModel Model { get; }
        private readonly PlayerStateView _playerStateView;
        private readonly PlayerTakingBonusesView _playerTakeBonusesView;
        private int _attackDeBuff;
        private GridController _gridController;



        public bool IsAlive => Model.Hp > 0;


        public PlayerController(PlayerModel model, PlayerStateView playerStateView, PlayerTakingBonusesView playerTakeBonusesView)
        {
            _playerStateView = playerStateView;
            Model = model;
            _playerTakeBonusesView = playerTakeBonusesView;

            for (var i = 0; i < Model.TakingBonuses.Length; i++)
            {
                if (Model.TakingBonuses[i] == null)
                {
                    _playerTakeBonusesView.ClearBonus(i);
                }

                else
                {
                    if (_bonusViewsBuilder != null)
                        _playerTakeBonusesView.SetInitialized(i,
                            _bonusViewsBuilder.GetBonusIcon(Model.TakingBonuses[i]),
                            Model.TakingBonuses[i].CountOfUses.Value);
                }
            }

            Model.Hp.OnChange += OnHpChanged;
            Model.Damage.OnChange += OnDmgChanged;

            OnHpChanged(Model.Hp.Value);
            OnDmgChanged(Model.Damage.Value);
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
            for (var i = 0; i < Model.TakingBonuses.Length; i++)
            {
                var bonus = Model.TakingBonuses[i];
                if (bonus is not { IsActive: true }) continue;
                switch (bonus)
                {
                    case HawkEyeBonus eye:
                        eye.AttackedEnemyCoordinates = coords;
                        bonus.IsActive = false;
                        _gridController.ActivateHawkEye(eye, enemy);

                        return;
                    case HalfHpBonus:
                        RemoveBonus(i);
                        enemy.TakeDamage(enemy.Hp.Value / 2f);
                        return;
                }
            }




            enemy.TakeDamage(Model.Damage.Value);


            if (!(enemy.Hp.Value > 0)) return;
            {
                for (var i = 0; i < Model.TakingBonuses.Length; i++)
                {
                    var bonus = Model.TakingBonuses[i];
                    if (bonus is not ShieldBonus || !bonus.IsActive) continue;
                    bonus.CountOfUses--;

                    if (bonus.CountOfUses <= 0)
                    {
                        RemoveBonus(i);
                    }

                    return;
                }

                Model.Hp -= enemy.Dmg;
            }
        }


        public bool TryTakeBonus(BonusBase bonus)
        {
            for (var i = 0; i < Model.TakingBonuses.Length; i++)
            {
                if (Model.TakingBonuses[i] != null) continue;
                Model.TakingBonuses[i] = bonus;
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
            Model.TakingBonuses[index] = null;
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

            Model.Damage -= attackModification;

            if (Model.Damage < GameConfig.PlayerMinPower)
            {
                Model.Damage.Value = GameConfig.PlayerMinPower;
            }
        }


        internal void ActivateBonus(int index)
        {
            if (Model.TakingBonuses[index] == null || Model.TakingBonuses[index].IsActive)
                return;

            var bonus = Model.TakingBonuses[index];

            bonus.IsActive = true;

            if (bonus is RepairBonus repair)
            {
                bonus.CountOfUses--;
                Model.Hp += Mathf.Round(UnityEngine.Random.Range(repair.MinHealValue, repair.MaxHealValue));
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
            Model.Hp.OnChange -= OnHpChanged;
            Model.Damage.OnChange -= OnDmgChanged;
        }


    }
}
