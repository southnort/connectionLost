using ConnectionLost.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Yrr.Core;


namespace ConnectionLost.Models
{
    [Serializable]
    public sealed class CellModel
    {
        private readonly CellModel[] _neighbours = new CellModel[6];

        public HexCoordinates Coordinates { get; set; }
        public CellStates CurrentState { get; private set; }
        public ICellContent CellContent { get; set; }
        public int BlocksCount { get; private set; }

        public event Action OnCellStateChanged;


        public void SetNewState(CellStates newState)
        {
            if (newState == CurrentState || CurrentState == CellStates.Empty) return;
            CurrentState = newState;

            if (IsBlocked()) return;
            OnCellStateChanged?.Invoke();
        }

        public void SetBlock()
        {
            BlocksCount++;

            if (BlocksCount == 1 && CurrentState != CellStates.Empty)
                OnCellStateChanged?.Invoke();
        }

        public void SetUnblock()
        {
            if (BlocksCount < 1) return;
            BlocksCount--;

            if (BlocksCount == 0 && CurrentState != CellStates.Empty)
                OnCellStateChanged?.Invoke();
        }

        public IEnumerable<CellModel> GetNeighboursList()
        {
            return _neighbours.Where(x => x != null);
        }

        public CellModel GetNeighbour(HexDirection direction)
        {
            return _neighbours[(int)direction];
        }

        public CellModel GetRandomNeighbour()
        {
            return _neighbours.Where(x => x != null).ToList().GetRandomItem();
        }

        public void SetNeighbour(HexDirection direction, CellModel cell)
        {
            if (cell == null) return;

            _neighbours[(int)direction] = cell;
            cell._neighbours[(int)direction.Opposite()] = this;
        }

        public void RemoveNeighbours()
        {
            for (var i = 0; i < _neighbours.Length; i++)
            {
                if (_neighbours[i] == null) continue;

                var direction = (HexDirection)i;
                _neighbours[i]._neighbours[(int)direction.Opposite()] = null;
                _neighbours[i] = null;
            }
        }

        public bool IsBlocked()
        {
            if (CurrentState == CellStates.Empty) return false;
            if (CurrentState == CellStates.HaveContent && !CellContent.IsCanBlocked)
                return false;

            return BlocksCount > 0;
        }

        public bool IsBlockerContent()
        {
            return (CurrentState == CellStates.HaveContent && CellContent.IsBlock);
        }
    }
}
