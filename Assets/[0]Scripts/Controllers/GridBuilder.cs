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

        private Dictionary<HexCoordinates, CellController> _controllersMap;
        private Dictionary<LineKey, LineController> _linesMap;
      
        public void BuildGrid(GridModel gridModel, GridController controller)
        {
            transform.ClearChildren();
            _controllersMap = new Dictionary<HexCoordinates, CellController>();
            _linesMap = new Dictionary<LineKey, LineController>();

            foreach (var cell in gridModel.Cells)
            {
                CreateCell(cell);
            }

            foreach (var cell in gridModel.Cells)
            {
                CreateLines(cell);
            }

            controller.SetCellsAndLines(_controllersMap, _linesMap);
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
