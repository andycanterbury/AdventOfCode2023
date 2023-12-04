using NUnit.Framework;
using System;
using System.Linq;

namespace AdventOfCode2023
{
    public class Day4
    {
        public static string[] fileData;

        [SetUp]
        public void Setup()
        {
            fileData = Utilities.InputLoader.LoadFile("Data/Day4.txt");
        }

        [Test]
        public void Part1()
        {
            var points = 0;
            var cardPoints = 0;
            foreach(var line in fileData)
            {
                var card = line.Split(":")[1];
                var cardData = card.Split("|");
 
                var winners = cardData[0].Trim().Split(" ");
                var numbers = cardData[1].Trim().Split(" ");

                foreach (var winner in winners)
                {
                    if (winner == "") continue;
                    if (numbers.Contains(winner))
                    { 
                        if (cardPoints == 0)
                        {
                            cardPoints = 1;
                        } else
                        {
                            cardPoints *= 2;
                        }
                    }
                }
                points += cardPoints;
                cardPoints = 0;
            }
            Console.WriteLine(points);
        }

        [Test]
        public void Part2()
        {
            var cardCount = 0;
            var cardArray = new int[fileData.Length];

            var cardPoints = 0;
            for (int i = 0; i < fileData.Length; i++)
            {
                var card = fileData[i].Split(":")[1];
                var cardData = card.Split("|");

                cardArray[i]++;

                var winners = cardData[0].Trim().Split(" ");
                var numbers = cardData[1].Trim().Split(" ");

                foreach (var winner in winners)
                {
                    if (winner == "") continue;
                    if (numbers.Contains(winner))
                    {
                        cardPoints++;
                    }
                }

                for (int j = 1; j <= cardPoints; j++)
                {
                    if ((i + j) < fileData.Length)
                    {
                        cardArray[i + j] += cardArray[i];
                    }
                }
                cardPoints = 0;
            }

            cardCount = cardArray.Sum();
            Console.WriteLine(cardCount);
        }
    }
}
