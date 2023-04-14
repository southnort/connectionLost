using System;
using ConnectionLost.Models;
using ConnectionLost.Views;
using ConnectionLost.Core;
using System.Collections.Generic;

namespace ConnectionLost.Controllers
{
    internal sealed class CellController
    {
        public CellModel Model { get; set; }
        public CellView View { get; set; }
        public List<Line> Lines { get; set; } = new();
        public List<CellController> NeighbourCells { get; } = new();


        public void HandleClick()
        {
            switch (Model.CurrentState)
            {
                case CellStates.Open:
                    OpenCell();
                    View.SetState(Model.CurrentState);
                    break;
                case CellStates.Undefined:
                    break;
                case CellStates.Closed:
                    break;
                case CellStates.Blocked:
                    break;
                case CellStates.Empty:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetOpened()
        {
            if (Model.CurrentState is CellStates.Empty or CellStates.Blocked) return;

            Model.CurrentState = CellStates.Open;
            UpdateViews();            
        }

        private void OpenCell()
        {
            if (Model.CellContent != null) return;
            SetEmpty();
            foreach (var n in NeighbourCells)
            {
                n.SetOpened();
            }
        }

        private void SetEmpty()
        {
            Model.CurrentState = CellStates.Empty;
            UpdateViews();
        }

        public void UpdateViews()
        {
            View.SetState(Model.CurrentState);
            foreach (var line in Lines)
            {
                line.SetState(Model.CurrentState);
            }
        }
    }
}
