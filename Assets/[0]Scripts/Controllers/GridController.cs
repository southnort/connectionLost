using ConnectionLost.Core;
using ConnectionLost.Models;
using System.Collections.Generic;
using UnityEngine;


namespace ConnectionLost.Controllers
{
    public sealed class GridController : MonoBehaviour
    {
        [SerializeField] private EnemiesSpawner enemySpawner;

        private Dictionary<HexCoordinates, CellController> _cells;
        private Dictionary<LineKey, LineController> _lines;
        private Dictionary<HexCoordinates, IContentController> _contents;
        private readonly PlayerModel _player = new();

        internal void Initialize(Dictionary<HexCoordinates, CellController> controllersMap, Dictionary<LineKey, LineController> linesMap)
        {
            _cells = controllersMap;
            _lines = linesMap;
            _contents = new Dictionary<HexCoordinates, IContentController>();
        }

        internal void ClickOnCell(HexCoordinates coords)
        {
            var result = _cells[coords].ClickOnCell(_player);
            HandleClickResult(coords, result);
        }

        private void HandleClickResult(HexCoordinates clickedCellCoords, ClickResult result)
        {
            if (!result.NeedUpdate) return;

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
            if (content is not EnemyBase enemy) return;
            var view = enemySpawner.CreateView(content);
            view.transform.position = coords.ToVector3();

            var controller = new EnemyController(view, enemy);
            _contents.Add(coords, controller);
        }

        private void OnDestroy()
        {
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
    }
}