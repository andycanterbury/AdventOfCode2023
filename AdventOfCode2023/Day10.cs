using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Utilities;

namespace AdventOfCode2023
{
    public class Day10
    {
        public static string[] fileData;
        public static char[] validUp = { '|', '7', 'F' };
        public static char[] validDown = { '|', 'L', 'J' };
        public static char[] validLeft = { '-', 'L', 'F' };
        public static char[] validRight = { '-', '7', 'J' };
        public static char[] insideChars = { '|', 'L', 'J' };

        [SetUp]
        public void Setup()
        {
            fileData = Utilities.InputLoader.LoadFile("Data/Day10.txt");
        }

        [Test]
        public void Part1()
        {
            var rows = fileData.Length;
            var cols = fileData[0].Length;
            var grid = new Matrix<char>(rows, cols);
            var startLocation = new MatrixLocation<char>();
            for(int i = 0; i < rows; i++) 
            {
                for(int j = 0; j < fileData[i].Length; j++)
                {
                    var gridData = fileData[i][j];
                    grid[i, j] = gridData;
                    if (gridData == 'S')
                    {
                        startLocation = new MatrixLocation<char> { Row = i, Column = j, Value = gridData };
                    }
                }
            }

            var path = TraversePath(grid, startLocation, 'S');

            Console.WriteLine(path.Count/2);
        }

        [Test]
        public void Part2()
        {
            var rows = fileData.Length;
            var cols = fileData[0].Length;
            var grid = new Matrix<char>(rows, cols);
            var startLocation = new MatrixLocation<char>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < fileData[i].Length; j++)
                {
                    var gridData = fileData[i][j];
                    grid[i, j] = gridData;
                    if (gridData == 'S')
                    {
                        startLocation = new MatrixLocation<char> { Row = i, Column = j, Value = gridData };
                    }
                }
            }

            var path = TraversePath(grid, startLocation, 'S');

            var insideCount = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    //is this part of the path?
                    if (!path.Any(p => p.Row == i && p.Column == j))
                    {
                        var location = new MatrixLocation<char> { Value = grid[i, j], Row = i, Column = j };
                        var isInside = IsLocationInside(grid, cols, path, location);
                        if (isInside)
                        {
                            insideCount++;
                        }
                    }
                }
            }

            Console.WriteLine(insideCount);
        }

        public List<MatrixLocation<char>> TraversePath(Matrix<char> grid, MatrixLocation<char> startLocation, char backDirection)
        {
            var nextDirection = ' ';
            var currentLocation = startLocation;
            var path = new List<MatrixLocation<char>>();

            do
            {
                path.Add(currentLocation);
                if (currentLocation.Value == 'S' && backDirection == 'S')
                {
                    nextDirection = DetermineStartDirection(grid, currentLocation);
                }

                switch (currentLocation.Value)
                {
                    case '|':
                        if (backDirection == 'U')
                        {
                            nextDirection = 'D';
                        }
                        else
                        {
                            nextDirection = 'U';
                        }
                        break;
                    case '-':
                        if (backDirection == 'L')
                        {
                            nextDirection = 'R';
                        }
                        else
                        {
                            nextDirection = 'L';
                        }
                        break;
                    case 'L':
                        if (backDirection == 'U')
                        {
                            nextDirection = 'R';
                        }
                        else
                        {
                            nextDirection = 'U';
                        }
                        break;
                    case 'J':
                        if (backDirection == 'U')
                        {
                            nextDirection = 'L';
                        }
                        else
                        {
                            nextDirection = 'U';
                        }
                        break;
                    case '7':
                        if (backDirection == 'D')
                        {
                            nextDirection = 'L';
                        }
                        else
                        {
                            nextDirection = 'D';
                        }
                        break;
                    case 'F':
                        if (backDirection == 'D')
                        {
                            nextDirection = 'R';
                        }
                        else
                        {
                            nextDirection = 'D';
                        }
                        break;
                }

                var neighbor = new MatrixLocation<char>();
                if (nextDirection == 'U')
                {
                    neighbor = grid.GetNeighborAbove(currentLocation.Row, currentLocation.Column);
                    backDirection = 'D';
                }
                if (nextDirection == 'D')
                {
                    neighbor = grid.GetNeighborBelow(currentLocation.Row, currentLocation.Column);
                    backDirection = 'U';
                }
                if (nextDirection == 'L')
                {
                    neighbor = grid.GetNeighborLeft(currentLocation.Row, currentLocation.Column);
                    backDirection = 'R';
                }
                if (nextDirection == 'R')
                {
                    neighbor = grid.GetNeighborRight(currentLocation.Row, currentLocation.Column);
                    backDirection = 'L';
                }
                currentLocation = neighbor;
            } while (currentLocation.Value != 'S');
            return path;
        }

        public char DetermineStartDirection(Matrix<char> grid, MatrixLocation<char> startLocation)
        {
            var nextDirection = ' ';
            var neighbor = grid.GetNeighborAbove(startLocation.Row, startLocation.Column);
            if (validUp.Contains(neighbor.Value))
            {
                nextDirection = 'U';
            }
            neighbor = grid.GetNeighborBelow(startLocation.Row, startLocation.Column);
            if (validDown.Contains(neighbor.Value))
            {
                nextDirection = 'D';
            }
            neighbor = grid.GetNeighborLeft(startLocation.Row, startLocation.Column);
            if (validLeft.Contains(neighbor.Value))
            {
                nextDirection = 'L';
            }
            neighbor = grid.GetNeighborRight(startLocation.Row, startLocation.Column);
            if (validRight.Contains(neighbor.Value))
            {
                nextDirection = 'R';
            }
            return nextDirection;
        }

        public bool IsLocationInside(Matrix<char> grid, int gridWidth, List<MatrixLocation<char>> path, MatrixLocation<char> location)
        {
            var pathCrossings = 0;
            for(int i = location.Column + 1; i < gridWidth; i++)
            {
                if (path.Any(p => p.Value == grid[location.Row, i] && p.Row == location.Row && p.Column == i && insideChars.Contains(p.Value)))
                {
                    pathCrossings++;
                }
            }
            if (pathCrossings % 2 == 1)
            {
                return true;
            }
            return false;
        }
    }
}
