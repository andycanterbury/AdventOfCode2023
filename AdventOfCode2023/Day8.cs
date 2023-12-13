using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;

namespace AdventOfCode2023
{
    public class Day8
    {
        public static string[] fileData;

        [SetUp]
        public void Setup()
        {
            fileData = Utilities.InputLoader.LoadFile("Data/Day8.txt");
        }

        [Test]
        public void Part1()
        {
            var map = new Dictionary<string, Node>();
            var turns = fileData[0];
            for(int i = 2; i < fileData.Length; i++) 
            {
                var data = fileData[i].Split("=");
                var directions = data[1].Replace("(", "").Replace(")", "").Split(",");
                map.Add(data[0].Trim(), new Node { Left = directions[0].Trim(), Right = directions[1].Trim() });
            }

            var steps = 0;
            var current = "AAA";
            var turnIndex = 0;
            while(current != "ZZZ")
            {
                var direction = turns[turnIndex];
                if (direction == 'L')
                {
                    current = map[current].Left;
                }
                if (direction == 'R')
                {
                    current = map[current].Right;
                }
                turnIndex++;
                if (turnIndex >= turns.Length) 
                {
                    turnIndex = 0;
                }
                steps++;
            }
            
            Console.WriteLine(steps);
        }

        [Test]
        public void Part2()
        {
            var map = new Dictionary<string, Node>();
            var turns = fileData[0];
            var current = new List<string>();
            for (int i = 2; i < fileData.Length; i++)
            {
                var data = fileData[i].Split("=");
                var directions = data[1].Replace("(", "").Replace(")", "").Split(",");
                map.Add(data[0].Trim(), new Node { Left = directions[0].Trim(), Right = directions[1].Trim() });
                if (data[0].Trim().EndsWith('A'))
                {
                    current.Add(data[0].Trim());
                }
            }

            var stepList = new List<long>();
            for(int i = 0; i < current.Count; i++)
            {
                var steps = 0;
                var turnIndex = 0;
                while (!current[i].EndsWith("Z"))
                {
                    var direction = turns[turnIndex];
                    if (direction == 'L')
                    {
                        current[i] = map[current[i]].Left;
                    }
                    if (direction == 'R')
                    {
                        current[i] = map[current[i]].Right;
                    }
                    turnIndex++;
                    if (turnIndex >= turns.Length)
                    {
                        turnIndex = 0;
                    }
                    steps++;
                }
                stepList.Add(steps);
            }

            var answer = Utilities.LeastCommonMultiple.LCM(stepList);

            Console.WriteLine(answer);
        }

        public class Node
        {
            public string Left { get; set; }
            public string Right { get; set; }
        }

    }
}
