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

        private Dictionary<HexCoordinates, CellController> _drawableCells;

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
            _drawableCells = new Dictionary<HexCoordinates, CellController>();

            var generator = new GridGenerator();
            var stats = new GridStats(5, 10, 40);
            var grid = generator.GenerateRandomGrid(stats);

            DrawGrid(grid);
            gridController.SetCells(_drawableCells);
        }

        public void DrawGrid(GridModel gridModel)
        {
            foreach (var cell in gridModel.Cells)
            {
                CreateCell(cell);
            }

            foreach (var cell in gridModel.Cells)
            {
                CreateLine(cell);
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
            cell.SetLabel(model.Coordinates.ToString());

            var controller = new CellController
            {
                Model = model,
                View = cell
            };
            _drawableCells.Add(model.Coordinates, controller);
        }

        private void CreateLine(CellModel cell)
        {
            foreach (var other in cell.GetNeighboursList())
            {
                if (other == null) continue;

                var line = Instantiate(linePrefab, transform);
                var pos1 = _drawableCells[cell.Coordinates].View.transform.position;
                var pos2 = _drawableCells[other.Coordinates].View.transform.position;

                line.SetLine(pos1, pos2);
            }

        }
    }
}
