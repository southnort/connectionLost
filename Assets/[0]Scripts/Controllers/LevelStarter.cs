using ConnectionLost.Models;
using UnityEngine;
using ConnectionLost.Core;
using ConnectionLost.Views;

namespace ConnectionLost.Controllers
{
    internal sealed class LevelStarter : MonoBehaviour
    {
        [SerializeField] private GridBuilder gridBuilder;
        [SerializeField] private GridController gridController;
        [SerializeField] private PlayerStateView playerStateView;
        private PlayerController _playerController;

        private void Start()
        {
            StartLevel();
        }

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
            _playerController = new PlayerController(player, playerStateView);
            var generator = new GridGenerator();
            var gridFactory = new GridStatsFactory();
            var stats = gridFactory.BuildGridStats(playerData.CurrentDifficult);
            var grid = generator.GenerateRandomGrid(stats);

            gridBuilder.BuildGrid(grid, gridController);
            gridController.SetPlayer(_playerController);
        }

        private void OnDestroy()
        {
            _playerController.Dispose();
        }
    }
}
