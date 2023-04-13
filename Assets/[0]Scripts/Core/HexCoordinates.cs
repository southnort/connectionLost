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
            var y = position.z / (GameConfig.OuterRadius * 1.5f);

            var tempMod = position.x / (GameConfig.InnerRadius * 2f);
            var x = tempMod - (y % 2 * 0.5f);



            var iX = Mathf.RoundToInt(x);
            var iY = Mathf.RoundToInt(y);

            return new HexCoordinates(iX, iY);

            //var x = position.x / (GameConfig.InnerRadius * 2f);
            //var y = -x;
            //var offset = position.z / (GameConfig.OuterRadius * 3f);
            //x -= offset;
            //y -= offset;

            //var iX = Mathf.RoundToInt(x);
            //var iY = Mathf.RoundToInt(y);
            //var iZ = Mathf.RoundToInt(-x - y);

            //return new HexCoordinates(iX, iZ);
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