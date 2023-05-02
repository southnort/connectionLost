using ConnectionLost.Core;
using ConnectionLost.Models;
using ConnectionLost.Views;
using UnityEngine;
using Yrr.UI;


namespace ConnectionLost.Controllers
{
    internal sealed class LevelStarter : MonoBehaviour
    {
        [SerializeField] private GridBuilder gridBuilder;
        [SerializeField] private GridController gridController;
        [SerializeField] private PlayerStateView playerStateView;
        [SerializeField] private PlayerTakingBonusesView playerTakingBonusesView;
        [SerializeField] private UIManager uiManager;
        private PlayerController _playerController;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartLevel();
            }
        }

        public void StartLevel()
        {
            var playerData = PlayerData.CurrentData;
            var player = new PlayerModel(playerData);
            _playerController = new PlayerController(player, playerStateView, playerTakingBonusesView);
            var generator = new GridGenerator();
            var gridFactory = new GridStatsFactory();
            var stats = gridFactory.BuildGridStats(playerData.CurrentDifficult);
            var grid = generator.GenerateRandomGrid(stats);

            gridBuilder.BuildGrid(grid, gridController);
            gridController.SetPlayer(_playerController);
            gridController.SetUiManager(uiManager);
        }

        private void OnDestroy()
        {
            if (_playerController == null) return;
            _playerController.Dispose();
        }
    }
}
