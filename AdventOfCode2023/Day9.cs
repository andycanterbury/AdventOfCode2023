using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023
{
    public class Day9
    {
        public static string[] fileData;

        [SetUp]
        public void Setup()
        {
            fileData = Utilities.InputLoader.LoadFile("Data/Day9.txt");
        }

        [Test]
        public void Part1()
        {
            var total = 0;
            foreach (var history in fileData)
            {
                var values = history.Split(" ").Select(h => int.Parse(h)).ToList();
                total += PredictNextValue(values);
            }
            Console.WriteLine(total);
        }

        [Test]
        public void Part2()
        {
            var total = 0;
            foreach (var history in fileData)
            {
                var values = history.Split(" ").Select(h => int.Parse(h)).ToList();
                total += PredictPreviousValue(values);
            }
            Console.WriteLine(total);
        }

        public int PredictNextValue(List<int> values)
        {
            var newValues = new List<int>();
            var next = 0;
            for(int i = 0; i < values.Count-1; i++)
            {
                newValues.Add(values[i+1] - values[i]);
            }
            if(!newValues.All(v => v == 0))
            {
                next = PredictNextValue(newValues);
            }
            return values.Last() + next;
        }

        public int PredictPreviousValue(List<int> values)
        {
            var newValues = new List<int>();
            var next = 0;
            for (int i = 0; i < values.Count - 1; i++)
            {
                newValues.Add(values[i + 1] - values[i]);
            }
            if (!newValues.All(v => v == 0))
            {
                next = PredictPreviousValue(newValues);
            }
            return values.First() - next;
        }
    }
}
