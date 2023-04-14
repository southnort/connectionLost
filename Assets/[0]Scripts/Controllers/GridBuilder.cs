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
        private Dictionary<LineKey, Line> _linesMap;

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
            _linesMap = new Dictionary<LineKey, Line>();

            var generator = new GridGenerator();
            var stats = new GridStats(5, 10, 30);
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

                if (_linesMap.ContainsKey(key1) || _linesMap.ContainsKey(key2)) continue;

                var line = Instantiate(linePrefab, transform);
                _linesMap.Add(key1, line);
                _linesMap.Add(key2, line);

                var pos1 = _controllersMap[cell.Coordinates].View.transform.position;
                var pos2 = _controllersMap[other.Coordinates].View.transform.position;               

                line.SetLine(pos1, pos2);
            }
        }
    }
}
