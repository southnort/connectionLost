using ConnectionLost.Core;
using System.Collections.Generic;
using UnityEngine;


namespace ConnectionLost.Controllers
{
    public sealed class GridController : MonoBehaviour
    {
        private Dictionary<HexCoordinates, CellController> _cells;
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

            _cells[coords].HandleClick();
        }


        internal void SetCells(Dictionary<HexCoordinates, CellController> cells)
        {
            _cells = cells;
        }


    }
}