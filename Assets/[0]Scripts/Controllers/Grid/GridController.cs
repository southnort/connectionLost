using ConnectionLost.Core;
using ConnectionLost.Models;
using ConnectionLost.UI;
using ConnectionLost.Views;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core;
using UnityEngine;
using Yrr.UI;



namespace ConnectionLost.Controllers
{
    internal sealed class GridController : MonoBehaviour
    {
        [SerializeField] private EnemiesSpawner enemySpawner;
        [SerializeField] private BonusViewsBuilder bonusBuilder;
        [SerializeField] private UIManager uiManager;

        internal event Action OnEndOfPlayerTurn;

        private Dictionary<HexCoordinates, IContentController> _contents;
        private Dictionary<HexCoordinates, CellController> _cells;
        private PlayerController _player;
        private EnemyControllerFactory _enemyFactory;

        private HashSet<EnemyController> _activeEnemies;

        private void Awake()
        {
            _contents = new Dictionary<HexCoordinates, IContentController>();
            _activeEnemies = new HashSet<EnemyController>();
            _enemyFactory = new EnemyControllerFactory();
        }

        private void OnDestroy()
        {
            if (_cells == null) return;

            foreach (var cell in _cells.Values)
            {
                cell.Dispose();
            }
            
            foreach (var content in _contents.Values)
            {
                content.Dispose();
            }
        }

        internal void SetCells(Dictionary<HexCoordinates, CellController> controllersMap)
        {
            _cells = controllersMap;
        }

        internal void SetPlayer(PlayerController player)
        {
            _player = player;
            _player.SetBonusBuilder(bonusBuilder);
            _player.SetGridController(this);
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

        private void HandleGameTick()
        {
            OnEndOfPlayerTurn?.Invoke();
        }

        public void ClickOnActivateBonus(int bonusIndex)
        {
            _player.ActivateBonus(bonusIndex);
        }


        private void HandleClickResult(HexCoordinates clickedCellCoords, ClickResult result)
        {
            if (!result.NeedUpdate) return;

            if (result.ForceUpdate)
            {
                _contents[clickedCellCoords].Dispose();
                _contents.Remove(clickedCellCoords);

                ShowContent(clickedCellCoords, result.CellContent);

                return;
            }

            if (_contents.ContainsKey(clickedCellCoords) && result.CellContent == null)
            {
                _contents[clickedCellCoords].Dispose();
                _contents.Remove(clickedCellCoords);
            }

            if (result.CellContent != null && !_contents.ContainsKey(clickedCellCoords))
            {
                ShowContent(clickedCellCoords, result.CellContent);
            }
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

            var controller = _enemyFactory.CreateEnemyController(view, enemy);
            controller.SetGridController(this);
            _activeEnemies.Add(controller);
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




        internal void RemoveActiveEnemy(EnemyController enemy)
        {
            _activeEnemies.Remove(enemy);
        }

        internal IEnumerable<EnemyController> GetActiveEnemies()
        {
            return _activeEnemies;
        }

        internal PlayerController GetPlayer()
        {
            return _player;
        }

        private void OnPlayerDeath()
        {
            uiManager.GoToWindow<GameOverCanvas>();
        }

        public static void OnCoreDeath()
        {
            Debug.LogError("Core dead");
        }
    }
}
