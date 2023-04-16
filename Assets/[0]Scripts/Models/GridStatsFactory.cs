using ConnectionLost.Core;


namespace ConnectionLost.Models
{
    public sealed class GridStatsFactory
    {
        public GridStats BuildGridStats(GridDifficult difficult)
        {
            var width = 5;
            var height = 10;
            var cellsCount = GameConfig.CellsCountForGridDifficult * (int)difficult;

            var stats = new GridStats(width, height)
                .SetCellCount(cellsCount)
                .SetDifficult(difficult);

            return stats;
        }
    }
}
