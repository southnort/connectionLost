using ConnectionLost.Core;
using ConnectionLost.Models;
using ConnectionLost.Views;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace ConnectionLost.Controllers
{
    public sealed class GridController : MonoBehaviour
    {
        private Dictionary<HexCoordinates, CellController> _cells;
        private Dictionary<LineKey, Line> _linesMap;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleInput();
            }
        }

        internal void Initialize(Dictionary<HexCoordinates, CellController> cells, Dictionary<LineKey, Line> lines)
        {
            _cells = cells;
            _linesMap = lines;

            foreach (var cell in _cells)
            {
                UpdateCell(cell.Key);
            }
        }
        private void UpdateCell(HexCoordinates coords)
        {
            var cell = _cells[coords];
            cell.UpdateState();
            foreach (var neighbour in cell.Model.GetNeighboursList())
            {
                if (neighbour == null) continue;
                UpdateLine(cell.Model, neighbour);
            }
        }
       
        private void UpdateLine(CellModel cell1, CellModel cell2)
        {
            var line = _linesMap[new LineKey(cell1.Coordinates, cell2.Coordinates)];
            if (cell1.CurrentState == cell2.CurrentState)
                line.SetState(cell1.CurrentState);
            else if (cell1.CurrentState == CellStates.Blocked || cell2.CurrentState == CellStates.Blocked)
            {
                line.SetState(CellStates.Blocked);
            }
            else
            {
                line.SetState(CellStates.Open);
            }
        }



        private void HandleInput()
        {
            Ray inputRay = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(inputRay, out var hit))
            {
                TouchCell(hit.collider.transform.position);
            }
        }

        private void TouchCell(Vector3 pos)
        {
            pos = transform.InverseTransformPoint(pos);
            var coords = HexCoordinates.FromPosition(pos);


            var cell = _cells[coords].Model;
            if (cell.CurrentState == CellStates.Open)
            {
                OpenCell(cell);
            }
        }

        private void OpenCell(CellModel cell)
        {
            if (cell.CellContent != null) return;
            cell.CurrentState = CellStates.Empty;
            UpdateCell(cell.Coordinates);

            foreach (var neighbour in cell.GetNeighboursList().Where(neighbour => neighbour != null))
            {
                if (neighbour.CurrentState == CellStates.Closed)
                {
                    neighbour.CurrentState = CellStates.Open;
                }

                UpdateCell(neighbour.Coordinates);
            }
        }
    }
}