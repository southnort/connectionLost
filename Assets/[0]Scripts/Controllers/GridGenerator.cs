using System;
using System.Collections.Generic;
using ConnectionLost.Core;
using ConnectionLost.Models;
using Yrr.Core;


namespace ConnectionLost.Controllers
{
    internal sealed class GridGenerator
    {
        public GridModel GenerateRandomGrid(GridStats gridStats)
        {
            var plane = GenerateGridPlane(gridStats);
            var model = new GridModel
            {
                Cells = GenerateGridShape(plane, gridStats.CellsCount)
            };

            return model;
        }

        private static CellModel[] GenerateGridPlane(GridStats gridStats)
        {
            var cells = new CellModel[gridStats.Width * gridStats.Height];

            for (int z = 0, i = 0; z < gridStats.Height; z++)
            {
                for (var x = 0; x < gridStats.Width; x++)
                {
                    var cell = cells[i] = CreateCell(x, z);

                    if (x > 0)
                    {
                        cell.SetNeighbour(HexDirection.West, cells[i - 1]);
                    }

                    if (z > 0)
                    {
                        if ((z & 1) == 0)
                        {
                            cell.SetNeighbour(HexDirection.SouthEast, cells[i - gridStats.Width]);
                            if (x > 0)
                            {
                                cell.SetNeighbour(HexDirection.SouthWest, cells[i - gridStats.Width - 1]);
                            }
                        }

                        else
                        {
                            cell.SetNeighbour(HexDirection.SouthWest, cells[i - gridStats.Width]);
                            if (x < gridStats.Width - 1)
                            {
                                cell.SetNeighbour(HexDirection.SouthEast, cells[i - gridStats.Width + 1]);
                            }
                        }
                    }

                    i++;
                }
            }

            return cells;
        }

        private static CellModel CreateCell(int x, int z)
        {
            var cell = new CellModel
            {
                Coordinates = HexCoordinates.FromOffsetCoordinates(x, z)
            };

            return cell;
        }

        private static List<CellModel> GenerateGridShape(CellModel[] cells, int countOfCells)
        {
            var curCell = cells.GetRandomItem();
            var result = new List<CellModel>(countOfCells);

            while (countOfCells > 0)
            {
                if (!result.Contains(curCell))
                {
                    result.Add(curCell);
                    countOfCells--;
                }

                curCell = curCell.GetRandomNeighbour();
            }


            foreach (var c in cells)
            {
                if (!result.Contains(c))
                {
                    c.RemoveNeighbours();
                }
            }

            Array.Clear(cells, 0, cells.Length);
            return result;
        }
    }
}
