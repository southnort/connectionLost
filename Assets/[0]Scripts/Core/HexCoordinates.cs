using UnityEngine;


namespace ConnectionLost.Core
{
    [System.Serializable]
    public readonly struct HexCoordinates
    {
        public int X { get; }

        public int Z { get; }

        public HexCoordinates(int x, int z)
        {
            X = x;
            Z = z;
        }

        public static HexCoordinates FromOffsetCoordinates(int x, int z)
        {
            return new HexCoordinates(x, z);
        }

        public static HexCoordinates FromPosition(Vector3 position)
        {
            var x = position.x / (GameConfig.InnerRadius * 2f);
            var y = -x;
            var offset = position.z / (GameConfig.OuterRadius * 3f);
            x -= offset;
            y -= offset;

            var iX = Mathf.RoundToInt(x);
            var iY = Mathf.RoundToInt(y);
            var iZ = Mathf.RoundToInt(-x - y);

            if (iX + iY + iZ != 0)
            {
                Debug.LogError("Rounding error!");
            }

            return new HexCoordinates(iX, iZ);
        }


        public override string ToString()
        {
            return $"({X},{Z})";
        }

        public string ToStringOnSeparateLines()
        {
            return $"{X}\n{Z}";
        }
    }
}