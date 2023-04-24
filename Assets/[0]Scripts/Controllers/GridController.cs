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

        private Dictionary<HexCoordinates, CellController> _cells;
        private Dictionary<LineKey, LineController> _lines;
        private Dictionary<HexCoordinates, IContentController> _contents;
        private PlayerController _player;
        private List<EnemyBase> _activeEnemies;
        private List<HealerModel> _activeHealers;
        private UIManager _uiManager;


        internal void SetCellsAndLines(Dictionary<HexCoordinates, CellController> controllersMap, Dictionary<LineKey, LineController> linesMap)
        {
            _cells = controllersMap;
            _lines = linesMap;
            _contents = new Dictionary<HexCoordinates, IContentController>();
            _activeEnemies = new List<EnemyBase>();
            _activeHealers = new List<HealerModel>();
        }

        internal void SetPlayer(PlayerController player)
        {
            _player = player;
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
                for (int i = 0; i < suppressor.DebuffStrenght; i++)
                {
                    _player.AddAttackDebuff();
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
                for (int i = 0; i < suppressor.DebuffStrenght; i++)
                {
                    _player.RemoveAttackDebuff();
                }
            }

            if (cellContent is HealerModel healer)
            {
                _activeHealers.Remove(healer);
            }
            else if (cellContent is EnemyBase enemy)
            {
                _activeEnemies.Remove(enemy);
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
        }



        private void ShowContent(HexCoordinates coords, ICellContent content)
        {
            if (content is not EnemyBase enemy) return;
            var view = enemySpawner.CreateView(content);
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

        private void OnCoreDeath()
        {
            Debug.LogError("Core dead");
        }
    }
}