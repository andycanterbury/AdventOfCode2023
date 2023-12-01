using NUnit.Framework;
using System;

namespace AdventOfCode2023
{
    [Ignore("Ignore the Template")]
    public class DayTemplate
    {
        public static string[] fileData;

        [SetUp]
        public void Setup()
        {
            fileData = Utilities.InputLoader.LoadFile("Data/DayN.txt");
        }

        [Test]
        public void Part1()
        {
            var answer = 0;
            Console.WriteLine(answer);
        }

        [Test]
        public void Part2()
        {
            var answer = 0;
            Console.WriteLine(answer);
        }
    }
}
