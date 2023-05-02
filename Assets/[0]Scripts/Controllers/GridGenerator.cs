using System;
using System.Collections.Generic;
using System.Linq;
using ConnectionLost.Core;
using ConnectionLost.Models;
using ConnectionLost.Models.Enemy;
using Yrr.Utils;


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

            SetEnemiesInCells(model.Cells, gridStats);
            SetBonusesInCells(model.Cells, gridStats);
            SetWhiteNodes(model.Cells, gridStats);

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
                    curCell.SetNewState(CellStates.Closed);
                    countOfCells--;
                }

                curCell = curCell.GetRandomNeighbour();
            }

            result.Last().SetNewState(CellStates.Opened);

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

        private static void SetEnemiesInCells(List<CellModel> cells, GridStats gridStats)
        {
            var coreFactory = new CoreModelFactory();
            cells[0].CellContent = coreFactory.CreateEnemy(gridStats.Difficult);

            var enemyFactory = new RandomEnemyFactory();
            var countOfEnemies = GameConfig.EnemiesPercentByGrid * gridStats.CellsCount;
            while (countOfEnemies > 0)
            {
                var cell = cells.GetRandomItem();
                if (cell.CellContent == null && cell.CurrentState != CellStates.Opened)
                {
                    cell.CellContent = enemyFactory.CreateEnemy(gridStats.Difficult);
                    countOfEnemies--;
                }
            }
        }

        private static void SetBonusesInCells(List<CellModel> cells, GridStats gridStats)
        {
            var bonusFactory = new RandomBonusFactory();
            var countOfBonus = GameConfig.BonusesPercentByGrid * gridStats.CellsCount;
            while (countOfBonus > 0)
            {
                var cell = cells.GetRandomItem();
                if (cell.CellContent == null && cell.CurrentState != CellStates.Opened)
                {
                    cell.CellContent = bonusFactory.CreateBonus(gridStats.Difficult);
                    countOfBonus--;
                }
            }
        }

        private static void SetWhiteNodes(List<CellModel> cells, GridStats gridStats)
        {
            var nodesCount = (int)gridStats.Difficult;
            while (nodesCount > 0)
            {
                var cell = cells.GetRandomItem();
                if (cell.CellContent == null && cell.CurrentState != CellStates.Opened)
                {
                    WhiteNode whiteNode;
                    if (UnityEngine.Random.Range(0f, 1f) < 0.5f)
                    {
                        var bonusFactory = new RandomBonusFactory();
                        whiteNode = new WhiteNode(bonusFactory.CreateBonus(gridStats.Difficult));
                    }
                    else
                    {
                        var enemyFactory = new RandomEnemyFactory();
                        whiteNode = new WhiteNode(enemyFactory.CreateEnemy(gridStats.Difficult));
                    }

                    cell.CellContent = whiteNode;
                    nodesCount--;
                }
            }
        }
    }
}
