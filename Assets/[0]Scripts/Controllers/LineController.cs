using ConnectionLost.Core;
using ConnectionLost.Models;
using ConnectionLost.Views;
using System;
using UnityEngine;


namespace ConnectionLost.Controllers
{
    internal sealed class LineController : IDisposable
    {
        private CellModel _cell1;
        private CellModel _cell2;
        private Line _view;

        public LineController(CellModel cell1, CellModel cell2, Line view)
        {
            _cell1 = cell1;
            _cell2 = cell2;
            _view = view;

            _view.SetLine(_cell1.Coordinates.ToVector3(), _cell2.Coordinates.ToVector3());

            _cell1.OnCellStateChanged += UpdateVisual;
            _cell2.OnCellStateChanged += UpdateVisual;
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            Debug.Log("UpdateViewState");

            if (_cell1.IsBlockerContent() || _cell2.IsBlockerContent())
                _view.SetBlocked();
            else if (_cell1.IsBlocked() || _cell2.IsBlocked())
                _view.SetBlocked();
            else if (_cell1.CurrentState == CellStates.Empty && _cell2.CurrentState == CellStates.Empty)
                _view.SetEmpty();
            else if (_cell1.CurrentState == CellStates.Closed && _cell2.CurrentState == CellStates.Closed)
                _view.SetClosed();
            else
                _view.SetOpened();
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(_view.gameObject);
            _cell1.OnCellStateChanged -= UpdateVisual;
            _cell2.OnCellStateChanged -= UpdateVisual;
        }
    }
}
