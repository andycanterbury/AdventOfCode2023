using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2023
{
    public class Day6
    {
        public static string[] fileData;

        [SetUp]
        public void Setup()
        {
            fileData = Utilities.InputLoader.LoadFile("Data/Day6.txt");
        }

        [Test]
        public void Part1()
        {
            var times = fileData[0].Split(":")[1].Trim().Split(" ").Where(t => t != "").ToList();
            var distances = fileData[1].Split(":")[1].Trim().Split(" ").Where(d => d != "").ToList();

            var wins = new List<int>();
            for(int i = 0; i< times.Count; i++) 
            {
                var winCount = 0;
                var totalTime = int.Parse(times[i]);
                var targetDistance = int.Parse(distances[i]);

                for(int c = 0; c <= totalTime; c++) 
                {
                    var distance = (totalTime - c) * c;
                    if (distance > targetDistance) 
                    {
                        winCount++;
                    }
                }
                wins.Add(winCount);
            }

            var waysToWin = 0;
            foreach(var way in wins) 
            {
                if (waysToWin == 0)
                {
                    waysToWin = way;
                } else
                {
                    waysToWin *= way;
                }

            }
            Console.WriteLine(waysToWin);
        }

        [Test]
        public void Part2()
        {
            var time = fileData[0].Split(":")[1].Replace(" ","");
            var distance = fileData[1].Split(":")[1].Replace(" ", "");

            long winCount = 0;
            var totalTime = long.Parse(time);
            var targetDistance = long.Parse(distance);

            for (long c = 0; c <= totalTime; c++)
            {
                var dist = (totalTime - c) * c;
                if (dist > targetDistance)
                {
                    winCount++;
                }
            }

            Console.WriteLine(winCount);
        }
    }
}
