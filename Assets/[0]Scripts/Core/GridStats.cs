

namespace ConnectionLost.Core
{
    public sealed class GridStats
    {
        public int Width { get; }
        public int Height { get; }
        public int CellsCount { get; internal set; }
        public GridDifficult Difficult { get; internal set; }

        public GridStats(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }

    public static class GridStatsBuilderExtensions
    {
        public static GridStats SetCellCount(this GridStats stats, int cellsCount)
        {
            var max = stats.Width * stats.Height;
            if (cellsCount > max)
            {
                cellsCount = max;
            }
            stats.CellsCount = cellsCount;

            return stats;
        }

        public static GridStats SetDifficult(this GridStats stats, GridDifficult difficult)
        {
            stats.Difficult = difficult;

            return stats;
        }
    }
}
