using ConnectionLost.Core;
using ConnectionLost.Models;
using ConnectionLost.Views;
using System;
using Unity.Plastic.Newtonsoft.Json.Bson;
using UnityEngine;
using Yrr.Utils;
using static UnityEditor.Experimental.GraphView.GraphView;


namespace ConnectionLost.Controllers
{
    internal sealed class PlayerController : IDisposable
    {
        private BonusViewsBuilder _bonusViewsBuilder;
        internal PlayerModel Model { get; private set; }
        private readonly PlayerStateView _playerStateView;
        private readonly PlayerTakedBonusesView _playerTakedBonusesView;
        private int _attackDebuff;
        private GridController _gridController;



        public bool IsAlive => Model.Hp > 0;


        public PlayerController(PlayerModel model, PlayerStateView playerStateView, PlayerTakedBonusesView playerTakedBonusesView)
        {
            _playerStateView = playerStateView;
            Model = model;
            _playerTakedBonusesView = playerTakedBonusesView;

            for (int i = 0; i < Model.TakedBonuses.Length; i++)
            {
                if (Model.TakedBonuses[i] == null)
                {
                    _playerTakedBonusesView.ClearBonus(i);
                }

                else
                {
                    _playerTakedBonusesView.SetInitialized(i,
                        _bonusViewsBuilder.GetBonusIcon(Model.TakedBonuses[i]),
                        Model.TakedBonuses[i].CountOfUses.Value);
                }
            }

            Model.Hp.OnChange += OnHpChanged;
            Model.Damage.OnChange += OnDmgChanged;

            OnHpChanged(Model.Hp.Value);
            OnDmgChanged(Model.Damage.Value);
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

        public void AttackEnemy(EnemyBase enemy)
        {
            for (int i = 0; i < Model.TakedBonuses.Length; i++)
            {
                var bonus = Model.TakedBonuses[i];
                if (bonus != null && bonus is HalfHpBonus && bonus.IsActive)
                {
                    RemoveBonus(i);
                    enemy.TakeDamage(enemy.Hp.Value / 2f);
                    return;
                }
            }


            enemy.TakeDamage(Model.Damage.Value);



            if (enemy.Hp.Value > 0)
            {
                for (int i = 0; i < Model.TakedBonuses.Length; i++)
                {
                    var bonus = Model.TakedBonuses[i];
                    if (bonus != null && bonus is ShieldBonus && bonus.IsActive)
                    {
                        bonus.CountOfUses--;

                        if (bonus.CountOfUses <= 0)
                        {
                            RemoveBonus(i);
                        }

                        return;
                    }
                }

                Model.Hp -= enemy.Dmg;
            }
        }


        public bool TryTakeBonus(BonusBase bonus)
        {
            for (int i = 0; i < Model.TakedBonuses.Length; i++)
            {
                if (Model.TakedBonuses[i] == null)
                {
                    Model.TakedBonuses[i] = bonus;
                    _playerTakedBonusesView.SetInitialized(i, _bonusViewsBuilder.GetBonusIcon(bonus), bonus.CountOfUses.Value);
                    bonus.CountOfUses.OnChange += (int x) =>
                    {
                        _playerTakedBonusesView.UpdateCount(i * 1, x);
                    };
                    return true;
                }
            }

            return false;
        }

        public void RemoveBonus(int index)
        {
            Model.TakedBonuses[index] = null;
            _playerTakedBonusesView.ClearBonus(index);
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

            Model.Damage -= attackModification;

            if (Model.Damage < GameConfig.PlayerMinStrenght)
            {
                Model.Damage.Value = GameConfig.PlayerMinStrenght;
            }
        }


        internal void ActivateBonus(int index)
        {
            if (Model.TakedBonuses[index] == null || Model.TakedBonuses[index].IsActive)
                return;

            var bonus = Model.TakedBonuses[index];

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
                _playerTakedBonusesView.UpdateCount(index, bonus.CountOfUses.Value);
            }

        }



        public void Dispose()
        {
            Model.Hp.OnChange -= OnHpChanged;
            Model.Damage.OnChange -= OnDmgChanged;
        }


    }
}
