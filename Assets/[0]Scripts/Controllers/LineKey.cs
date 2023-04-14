using ConnectionLost.Core;


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

        public override string ToString()
        {
            return $"{_coords1} {_coords2}";
        }
    }
}