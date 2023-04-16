using ConnectionLost.Core;
using ConnectionLost.Models;
using System.Collections.Generic;
using UnityEngine;
using Yrr.Core;

namespace ConnectionLost.Controllers
{
    internal sealed class GridBuilder : MonoBehaviour
    {
        [SerializeField] private GridElementsSpawner spawner;
        [SerializeField] private GridController gridController;

        private Dictionary<HexCoordinates, CellController> _controllersMap;
        private Dictionary<LineKey, LineController> _linesMap;

        private void Awake()
        {
            TestDraw();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TestDraw();
            }
        }

        private void TestDraw()
        {
            transform.ClearChildren();
            _controllersMap = new Dictionary<HexCoordinates, CellController>();
            _linesMap = new Dictionary<LineKey, LineController>();

            var generator = new GridGenerator();
            var stats = new GridStats(5, 10)
                .SetCellCount(35)
                .SetDifficult(GridDifficult.Tutorial);

            var grid = generator.GenerateRandomGrid(stats);

            DrawGrid(grid);
            gridController.Initialize(_controllersMap, _linesMap);
        }

        public void DrawGrid(GridModel gridModel)
        {
            foreach (var cell in gridModel.Cells)
            {
                CreateCell(cell);
            }

            foreach (var cell in gridModel.Cells)
            {
                CreateLines(cell);
            }
        }

        private void CreateCell(CellModel model)
        {
            var cell = spawner.CreateCell();
            cell.transform.SetParent(transform, false);
            cell.transform.localPosition = model.Coordinates.ToVector3();

            var controller = new CellController(model, cell);
            _controllersMap.Add(model.Coordinates, controller);
        }

        private void CreateLines(CellModel cell)
        {
            foreach (var other in cell.GetNeighboursList())
            {
                if (other == null) continue;

                var key = new LineKey(cell.Coordinates, other.Coordinates);
                if (_linesMap.ContainsKey(key)) continue;

                var line = spawner.CreateLine();
                line.transform.SetParent(transform, false);

                var controller = new LineController(cell, other, line);
                _linesMap.Add(key, controller);
            }
        }
    }
}
