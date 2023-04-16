using ConnectionLost.Core;
using System;

namespace ConnectionLost.Controllers
{
    internal readonly struct LineKey
    {
        private readonly HexCoordinates _coords1;

        private readonly HexCoordinates _coords2;

        public LineKey(HexCoordinates hc1, HexCoordinates hc2)
        {
            _coords1 = hc1;
            _coords2 = hc2;
        }

        public override bool Equals(object obj)
        {
            return obj switch
            {
                null => throw new NullReferenceException("Trying equals null"),

                LineKey other => (_coords1.Equals(other._coords1) && _coords2.Equals(other._coords2)) ||
                                 (_coords1.Equals(other._coords2) && _coords2.Equals(other._coords1)),

                _ => throw new InvalidOperationException($"Trying equals wrong type: {obj.GetType()}")
            };
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_coords1, _coords2);
        }
    }
}