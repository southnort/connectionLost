using System.Collections.Generic;
using ConnectionLost.Core;
using ConnectionLost.Models;
using UnityEngine;
using ConnectionLost.Views;
using Yrr.Core;


namespace ConnectionLost.Controllers
{
    internal sealed class GridBuilder : MonoBehaviour
    {
        [SerializeField] private CellView cellPrefab;
        [SerializeField] private Line linePrefab;
        [SerializeField] private GridController gridController;

        private Dictionary<HexCoordinates, CellController> _controllersMap;
        private HashSet<LineKey> _keysForLines;

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
            _keysForLines = new HashSet<LineKey>();

            var generator = new GridGenerator();
            var stats = new GridStats(5, 10, 40);
            var grid = generator.GenerateRandomGrid(stats);

            DrawGrid(grid);
            gridController.SetCells(_controllersMap);
        }

        public void DrawGrid(GridModel gridModel)
        {
            foreach (var cell in gridModel.Cells)
            {
                CreateCell(cell);
            }

            foreach (var cell in gridModel.Cells)
            {
                foreach (var n in cell.GetNeighboursList())
                {
                    if (n == null) continue;
                    _controllersMap[cell.Coordinates].NeighbourCells
                        .Add(_controllersMap[n.Coordinates]);
                }
            }

            foreach (var cell in gridModel.Cells)
            {
                CreateLines(cell);
            }

            foreach (var controller in _controllersMap.Values)
            {
                controller.UpdateViews();
            }
        }

        private void CreateCell(CellModel model)
        {
            var x = model.Coordinates.X;
            var z = model.Coordinates.Z;

            Vector3 pos;
            pos.x = (x + z % 2 * 0.5f) * (GameConfig.InnerRadius * 2f);
            pos.y = 0f;
            pos.z = z * (GameConfig.OuterRadius * 1.5f);

            var cell = Instantiate(cellPrefab, transform);
            cell.transform.localPosition = pos;

            var controller = new CellController
            {
                Model = model,
                View = cell
            };

            _controllersMap.Add(model.Coordinates, controller);
        }

        private void CreateLines(CellModel cell)
        {
            foreach (var other in cell.GetNeighboursList())
            {
                if (other == null) continue;

                var key1 = new LineKey(cell.Coordinates, other.Coordinates);
                var key2 = new LineKey(other.Coordinates, cell.Coordinates);

                if (_keysForLines.Contains(key1) || _keysForLines.Contains(key2)) continue;

                _keysForLines.Add(key1);
                _keysForLines.Add(key2);

                var line = Instantiate(linePrefab, transform);
                var pos1 = _controllersMap[cell.Coordinates].View.transform.position;
                var pos2 = _controllersMap[other.Coordinates].View.transform.position;

                _controllersMap[cell.Coordinates].Lines.Add(line);
                _controllersMap[other.Coordinates].Lines.Add(line);

                line.SetLine(pos1, pos2);
            }
        }

        private readonly struct LineKey
        {
            private readonly HexCoordinates _coords1;

            private readonly HexCoordinates _coords2;

            public LineKey(HexCoordinates hc1, HexCoordinates hc2)
            {
                _coords1 = hc1;
                _coords2 = hc2;
            }

            public override string ToString()
            {
                return $"{_coords1} {_coords2}";
            }
        }

    }
}
