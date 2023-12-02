using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AdventOfCode2023
{
    public class Day2
    {
        public static string[] fileData;

        [SetUp]
        public void Setup()
        {
            fileData = Utilities.InputLoader.LoadFile("Data/Day2.txt");
        }

        [Test]
        public void Part1()
        {
            var targetRed = 12;
            var targetGreen = 13;
            var targetBlue = 14;

            var validGames = new List<GameTotals>();
            var idSum = 0;

            foreach (var game in fileData) 
            {
                var totals = ParseGame(game);
                if (totals.MaxRed <= targetRed && totals.MaxGreen <= targetGreen && totals.MaxBlue <= targetBlue)
                {
                    validGames.Add(totals);
                    idSum += totals.GameId;
                }
            }

            Console.WriteLine(idSum);
        }

        [Test]
        public void Part2()
        {
            var powerSum = 0;
            foreach (var game in fileData)
            {
                var totals = ParseGame(game);
                var power = totals.MaxRed * totals.MaxGreen * totals.MaxBlue;
                powerSum += power;
            }
            Console.WriteLine(powerSum);
        }

        public GameTotals ParseGame(string game)
        {
            var totals = new GameTotals();

            //Get Game Id
            var idStart = game.IndexOf(" ");
            var idEnd = game.IndexOf(":");
            totals.GameId = int.Parse(game.Substring(idStart, idEnd - idStart));

            var gameData = game.Substring(idEnd+1);
            var draws = gameData.Split(';');
            foreach (var draw in draws)
            {
                var colors = draw.Split(',');
                foreach(var color in colors)
                {
                    var colorTotal = int.Parse(color.Trim().Split(' ')[0]);
                    if (color.Contains("red"))
                    {
                        if (totals.MaxRed < colorTotal)
                        {
                            totals.MaxRed = colorTotal;
                        }
                    }
                    if (color.Contains("blue"))
                    {
                        if (totals.MaxBlue < colorTotal)
                        {
                            totals.MaxBlue = colorTotal;
                        }
                    }
                    if (color.Contains("green"))
                    {
                        if (totals.MaxGreen < colorTotal)
                        {
                            totals.MaxGreen = colorTotal;
                        }
                    }
                }
            }

            return totals;
        }

        public class GameTotals
        {
            public int GameId { get; set; }
            public int MaxRed { get; set; } = 0;
            public int MaxGreen { get; set; } = 0;
            public int MaxBlue { get; set; } = 0;
        }
    }
}
