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
        public CellStates CurrentState { get; set; }
        public ICellContent CellContent { get; set; }
        public int BlocksCount { get; set; }



        public List<CellModel> GetNeighboursList()
        {
            return _neighbours.ToList();
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
    }
}
