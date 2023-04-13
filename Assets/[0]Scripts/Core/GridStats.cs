

namespace ConnectionLost.Core
{
    public class GridStats
    {
        public int Width { get; }
        public int Height { get; }
        public int CellsCount { get; }

        public GridStats(int width, int height, int cellsCount)
        {
            var max = width * height;
            if (cellsCount > max)
            {
                CellsCount = max;
            }

            Width = width;
            Height = height;
            CellsCount = cellsCount;
        }
    }
}
