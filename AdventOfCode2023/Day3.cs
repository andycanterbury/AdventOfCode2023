using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace AdventOfCode2023
{
    public class Day3
    {
        public static string[] fileData;

        [SetUp]
        public void Setup()
        {
            fileData = Utilities.InputLoader.LoadFile("Data/Day3.txt");
        }

        [Test]
        public void Part1()
        {
            var partNumberSum = 0;
            var schematic = new Matrix<char>(fileData.Length, fileData[0].Length);
            for(int i = 0; i < fileData.Length; i++)
            {
                for(int j = 0; j < fileData[i].Length; j++)
                {
                    schematic[i, j] = fileData[i][j];
                }
            }

            var currentNumber = "";
            var isPartNumber = false;

            for(int i = 0; i < fileData.Length; i++)
            {
                for (int j = 0; j < fileData[i].Length; j++)
                {
                    if (char.IsNumber(schematic[i,j]))
                    {
                        currentNumber += schematic[i, j];
                        var neighbors = schematic.GetNeighborsWithDiagonals(i, j);
                        if (neighbors.Any(n => !char.IsNumber(n.Value) && n.Value != '.' ))
                        {
                            isPartNumber = true;
                        }
                    } else
                    {
                        if (currentNumber != "")
                        {
                            if(isPartNumber)
                            {
                                partNumberSum += int.Parse(currentNumber);
                                isPartNumber = false;
                            }
                            currentNumber = "";
                        }
                    }
                }
                if (currentNumber != "")
                {
                    if (isPartNumber)
                    {
                        partNumberSum += int.Parse(currentNumber);
                        isPartNumber = false;
                    }
                    currentNumber = "";
                }
            }
            Console.WriteLine(partNumberSum);
        }

        [Test]
        public void Part2()
        {
            var gearRatioSum = 0;
            var schematic = new Matrix<char>(fileData.Length, fileData[0].Length);
            for (int i = 0; i < fileData.Length; i++)
            {
                for (int j = 0; j < fileData[i].Length; j++)
                {
                    schematic[i, j] = fileData[i][j];
                }
            }

            var currentNumber = "";
            var isPotentialGear = false;
            var potentialGearHub = new MatrixLocation<char>();
            var potentialGears = new List<PotentialGear>();
            for (int i = 0; i < fileData.Length; i++)
            {
                for (int j = 0; j < fileData[i].Length; j++)
                {
                    if (char.IsNumber(schematic[i, j]))
                    {
                        currentNumber += schematic[i, j];
                        var neighbors = schematic.GetNeighborsWithDiagonals(i, j);
                        if (neighbors.Any(n => n.Value == '*'))
                        {
                            isPotentialGear = true;
                            potentialGearHub = neighbors.First(n => n.Value == '*');
                        }
                    }
                    else
                    {
                        if (currentNumber != "")
                        {
                            if (isPotentialGear)
                            {
                                //is this a part of any known gears?
                                var gearHub = potentialGears.FirstOrDefault(g => g.GearHub.Row == potentialGearHub.Row && g.GearHub.Column == potentialGearHub.Column);
                                if (gearHub != null)
                                {
                                    gearHub.PartNumbers.Add(int.Parse(currentNumber));
                                } else
                                {
                                    potentialGears.Add(new PotentialGear { GearHub = potentialGearHub, PartNumbers = { int.Parse(currentNumber) } });
                                }
                                isPotentialGear = false;
                            }
                            currentNumber = "";
                        }
                    }
                }
                if (currentNumber != "")
                {
                    if (isPotentialGear)
                    {
                        //is this a part of any known gears?
                        var gearHub = potentialGears.FirstOrDefault(g => g.GearHub.Row == potentialGearHub.Row && g.GearHub.Column == potentialGearHub.Column);
                        if (gearHub != null)
                        {
                            gearHub.PartNumbers.Add(int.Parse(currentNumber));
                        }
                        else
                        {
                            potentialGears.Add(new PotentialGear { GearHub = potentialGearHub, PartNumbers = { int.Parse(currentNumber) } });
                        }
                        isPotentialGear = false;
                    }
                    currentNumber = "";
                }
            }

            foreach(var gear in  potentialGears)
            {
                if (gear.PartNumbers.Count == 2) 
                {
                    gearRatioSum += (gear.PartNumbers[0] * gear.PartNumbers[1]);
                }
            }
            Console.WriteLine(gearRatioSum);
        }

        public class PotentialGear
        {
            public List<int> PartNumbers {get; set; } = new List<int>();
            public MatrixLocation<char> GearHub { get; set; }
        }


    }
}
