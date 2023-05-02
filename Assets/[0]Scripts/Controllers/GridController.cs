using ConnectionLost.Core;
using ConnectionLost.Models;
using ConnectionLost.Models.Enemy;
using ConnectionLost.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yrr.Utils;
using Yrr.UI;


namespace ConnectionLost.Controllers
{
    public sealed class GridController : MonoBehaviour
    {
        [SerializeField] private EnemiesSpawner enemySpawner;
        [SerializeField] private BonusViewsBuilder bonusBuilder;

        private Dictionary<HexCoordinates, CellController> _cells;
        private Dictionary<LineKey, LineController> _lines;
        private Dictionary<HexCoordinates, IContentController> _contents;
        private PlayerController _player;
        private List<EnemyBase> _activeEnemies;
        private List<HealerModel> _activeHealers;
        private Dictionary<HawkEyeBonus, EnemyBase> _activeHawkEyes;
        private UIManager _uiManager;

        internal void SetCellsAndLines(Dictionary<HexCoordinates, CellController> controllersMap, Dictionary<LineKey, LineController> linesMap)
        {
            _cells = controllersMap;
            _lines = linesMap;
            _contents = new Dictionary<HexCoordinates, IContentController>();
            _activeEnemies = new List<EnemyBase>();
            _activeHealers = new List<HealerModel>();
            _activeHawkEyes = new Dictionary<HawkEyeBonus, EnemyBase>();
        }

        internal void SetPlayer(PlayerController player)
        {
            _player = player;
            _player.SetBonusBuilder(bonusBuilder);
            _player.SetGridController(this);
        }

        internal void SetUiManager(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        internal void ClickOnCell(HexCoordinates coords)
        {
            if (!_player.IsAlive)
            {
                OnPlayerDeath();
            }
            else
            {
                var result = _cells[coords].ClickOnCell(_player);
                HandleClickResult(coords, result);
                HandleGameTick();
            }
        }

        private void HandleClickResult(HexCoordinates clickedCellCoords, ClickResult result)
        {
            if (!result.NeedUpdate) return;

            if (result.ForceUpdate)
            {
                HandleContentRemove(_cells[clickedCellCoords].GetContent());
                _contents[clickedCellCoords].Dispose();
                _contents.Remove(clickedCellCoords);

                ShowContent(clickedCellCoords, result.CellContent);
                HandleContentSpawn(result.CellContent);

                return;
            }

            if (_contents.ContainsKey(clickedCellCoords) && result.CellContent == null)
            {
                HandleContentRemove(_cells[clickedCellCoords].GetContent());
                _contents[clickedCellCoords].Dispose();
                _contents.Remove(clickedCellCoords);
            }

            if (result.CellContent != null && !_contents.ContainsKey(clickedCellCoords))
            {
                ShowContent(clickedCellCoords, result.CellContent);
                HandleContentSpawn(result.CellContent);
            }
        }

        private void HandleContentSpawn(ICellContent cellContent)
        {
            if (cellContent is SuppressorModel suppressor)
            {
                for (int i = 0; i < suppressor.DeBuffPower; i++)
                {
                    _player.AddAttackDeBuff();
                }
            }


        }

        private void HandleContentRemove(ICellContent cellContent)
        {
            if (cellContent is CoreModel)
            {
                OnCoreDeath();
            }

            if (cellContent is SuppressorModel suppressor)
            {
                for (int i = 0; i < suppressor.DeBuffPower; i++)
                {
                    _player.RemoveAttackDeBuff();
                }
            }

            switch (cellContent)
            {
                case HealerModel healer:
                    _activeHealers.Remove(healer);
                    break;
                case EnemyBase enemy:
                    _activeEnemies.Remove(enemy);
                    break;
            }
        }

        private void HandleGameTick()
        {
            foreach (var heal in _activeHealers)
            {
                if (_activeEnemies.Any())
                    _activeEnemies.GetRandomItem().Hp.Value += heal.HealValue;
                else
                    _activeHealers.GetRandomItem().Hp.Value += heal.HealValue;
            }

            for (var i = 0; i < _player.Model.TakingBonuses.Length; i++)
            {
                var heal = _player.Model.TakingBonuses[i];
                if (heal is not RepairBonus repair || !heal.IsActive) continue;
                repair.CountOfUses--;
                _player.Model.Hp += Mathf.Round(Random.Range(repair.MinHealValue, repair.MaxHealValue));

                if (repair.CountOfUses <= 0)
                {
                    _player.RemoveBonus(i);
                }
            }

            var keys = _activeHawkEyes.Keys.ToList();
            foreach (var key in keys)
            {
                if (_activeHawkEyes[key] == null || _activeHawkEyes[key].Hp <= 0)
                {
                    _player.RemoveBonus(key.IndexInPlayerBonuses);
                    _activeHawkEyes.Remove(key);
                }

                else
                {
                    _activeHawkEyes[key].TakeDamage(key.DamagePerTick);
                    if (_activeHawkEyes[key].Hp <= 0)
                    {
                        _contents[key.AttackedEnemyCoordinates].Dispose();
                        _contents.Remove(key.AttackedEnemyCoordinates);
                        _cells[key.AttackedEnemyCoordinates].ClickOnCell(_player);
                    }
                    key.CountOfUses--;
                    if (key.CountOfUses <= 0)
                    {
                        _player.RemoveBonus(key.IndexInPlayerBonuses);
                        _activeHawkEyes.Remove(key);
                    }
                }
            }
        }


        public void ClickOnActivateBonus(int bonusIndex)
        {
            _player.ActivateBonus(bonusIndex);
        }





        private void ShowContent(HexCoordinates coords, ICellContent content)
        {
            switch (content)
            {
                case EnemyBase enemy:
                    SpawnEnemy(coords, enemy);
                    break;
                case BonusBase bonus:
                    SpawnBonus(coords, bonus);
                    break;
                case WhiteNode node:
                    SpawnWhiteNode(coords, node);
                    break;
            }
        }

        private void SpawnEnemy(HexCoordinates coords, EnemyBase enemy)
        {
            var view = enemySpawner.CreateView(enemy);
            view.transform.position = coords.ToVector3();

            var controller = new EnemyController(view, enemy);
            if (enemy is HealerModel healer)
            {
                _activeHealers.Add(healer);
            }
            else
            {
                _activeEnemies.Add(enemy);
            }
            _contents.Add(coords, controller);
        }

        private void SpawnBonus(HexCoordinates coords, BonusBase bonus)
        {
            var view = bonusBuilder.CreateView(bonus);
            view.transform.SetParent(transform);
            view.transform.position = coords.ToVector3();

            var controller = new BonusController(view);
            _contents.Add(coords, controller);
        }

        private void SpawnWhiteNode(HexCoordinates coords, WhiteNode whiteNode)
        {
            var view = bonusBuilder.CreateView(whiteNode);
            view.transform.SetParent(transform);
            view.transform.position = coords.ToVector3();

            var controller = new BonusController(view);
            _contents.Add(coords, controller);
        }

        public void ActivateHawkEye(HawkEyeBonus hawkEye, EnemyBase enemy)
        {
            _activeHawkEyes.Add(hawkEye, enemy);
        }


        private void OnDestroy()
        {
            if (_cells == null) return;

            foreach (var cell in _cells.Values)
            {
                cell.Dispose();
            }

            foreach (var line in _lines.Values)
            {
                line.Dispose();
            }

            foreach (var content in _contents.Values)
            {
                content.Dispose();
            }
        }


        private void OnPlayerDeath()
        {
            _uiManager.GoToWindow<GameOverCanvas>();
        }

        private static void OnCoreDeath()
        {
            Debug.LogError("Core dead");
        }
    }
}