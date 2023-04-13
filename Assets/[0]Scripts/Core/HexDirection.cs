

namespace ConnectionLost.Core
{
    public enum HexDirection
    {
        NorthEast,
        East,
        SouthEast,
        SouthWest,
        West,
        NorthWest
    }

    public static class HexDirectionExtensions
    {
        public static HexDirection Opposite(this HexDirection direction)
        {
            return (int)direction < 3 ? (direction + 3) : (direction - 3);
        }
    }
}