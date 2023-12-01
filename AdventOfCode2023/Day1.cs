using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023
{
    public class Day1
    {
        public static string[] fileData;

        [SetUp]
        public void Setup()
        {
            fileData = Utilities.InputLoader.LoadFile("Data/day1.txt");
        }

        [Test]
        public void Part1()
        {
            var lineNumbers = new List<int>();
            foreach (var line in fileData)
            {
                string number = line.First(c => char.IsDigit(c)).ToString();
                number += line.Last(c => char.IsDigit(c)).ToString();
                lineNumbers.Add(int.Parse(number));
            }

            Console.WriteLine(lineNumbers.Sum());
        }

        [Test]
        public void Part2()
        {
            var numbers = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            var lineNumbers = new List<int>();
            foreach (var line in fileData)
            {
                var number = StringToNumber(FirstIndexOfAny(line, numbers)).ToString();
                number += StringToNumber(LastIndexOfAny(line, numbers)).ToString();
                lineNumbers.Add(int.Parse(number));
            }

            Console.WriteLine(lineNumbers.Sum());
        }

        public string FirstIndexOfAny(string test, string[] values)
        {
            int first = -1;
            string numberFound = "";
            foreach (string item in values)
            {
                int i = test.IndexOf(item);
                if (i >= 0)
                {
                    if (first >= 0)
                    {
                        if (i < first)
                        {
                            first = i;
                            numberFound = item;
                        }
                    }
                    else
                    {
                        first = i;
                        numberFound = item;
                    }
                }
            }
            return numberFound;
        }

        public string LastIndexOfAny(string test, string[] values)
        {
            int last = -1;
            string numberFound = "";
            foreach (string item in values)
            {
                int i = test.LastIndexOf(item);
                if (i >= 0)
                {
                    if (last >= 0)
                    {
                        if (i > last)
                        {
                            last = i;
                            numberFound = item;
                        }
                    }
                    else
                    {
                        last = i;
                        numberFound = item;
                    }
                }
            }
            return numberFound;
        }

        public int StringToNumber(string numberString)
        {
            switch (numberString)
            {
                case "one":
                    return 1;
                case "two":
                    return 2;
                case "three":
                    return 3;
                case "four":
                    return 4;
                case "five":
                    return 5;
                case "six":
                    return 6;
                case "seven":
                    return 7;
                case "eight":
                    return 8;
                case "nine":
                    return 9;
            }
            return int.Parse(numberString);
        }
    }
}