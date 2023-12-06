using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using Utilities;

namespace AdventOfCode2023
{
    public class Day5
    {
        public static string[] fileData;

        [SetUp]
        public void Setup()
        {
            fileData = Utilities.InputLoader.LoadFile("Data/Day5.txt");
        }

        [Test]
        public void Part1()
        {
            //Parse the input
            var seedList = new List<long>();

            var seedSoilMap = new List<Mapping>();
            var soilFertilizerMap = new List<Mapping>();
            var fertilizerWater = new List<Mapping>();
            var waterLightMap = new List<Mapping>();
            var lightTempMap = new List<Mapping>();
            var tempHumidityMap = new List<Mapping>();
            var humidityLocationMap = new List<Mapping>();

            var whichMap = "";
            var incomingMapping = new Mapping();

            foreach(var line in fileData) 
            {
                if (line.StartsWith("seeds:"))
                {
                    seedList = fileData[0].Split(":")[1].Trim().Split(" ").Select(s => long.Parse(s)).ToList();
                    continue;
                }
                if (line == "") continue;
                if (line.EndsWith(":"))
                {
                    whichMap = line;
                    continue;
                }

                var values = line.Split(" ");
                var rangeLength = long.Parse(values[2]);
                incomingMapping.SourceRangeStart = long.Parse(values[1]);
                incomingMapping.SourceRangeEnd = incomingMapping.SourceRangeStart + rangeLength - 1;
                incomingMapping.DestinationRangeStart = long.Parse(values[0]);
                incomingMapping.DestinationRangeEnd = incomingMapping.DestinationRangeStart + rangeLength - 1;

                switch (whichMap)
                {
                    case "seed-to-soil map:":
                        seedSoilMap.Add(incomingMapping);
                        break;
                    case "soil-to-fertilizer map:":
                        soilFertilizerMap.Add(incomingMapping);
                        break;
                    case "fertilizer-to-water map:":
                        fertilizerWater.Add(incomingMapping);
                        break;
                    case "water-to-light map:":
                        waterLightMap.Add(incomingMapping);
                        break;
                    case "light-to-temperature map:":
                        lightTempMap.Add(incomingMapping);
                        break;
                    case "temperature-to-humidity map:":
                        tempHumidityMap.Add(incomingMapping);
                        break;
                    case "humidity-to-location map:":
                        humidityLocationMap.Add(incomingMapping);
                        break;
                }
                incomingMapping = new Mapping();

            }

            var shortestLocation = long.MaxValue;
            foreach(var seed in seedList)
            {
                var soil = ApplyMapping(seedSoilMap, seed);
                var fertilizer = ApplyMapping(soilFertilizerMap, soil);
                var water = ApplyMapping(fertilizerWater, fertilizer);
                var light = ApplyMapping(waterLightMap, water);
                var temp = ApplyMapping(lightTempMap, light);
                var humidity = ApplyMapping(tempHumidityMap, temp);
                var location = ApplyMapping(humidityLocationMap, humidity);

                if (location < shortestLocation) shortestLocation = location;
            }
            Console.WriteLine(shortestLocation);
        }

        [Test]
        public void Part2()
        {
            //Parse the input
            var seedList = new List<InputRange>();

            var seedSoilMap = new List<Mapping>();
            var soilFertilizerMap = new List<Mapping>();
            var fertilizerWater = new List<Mapping>();
            var waterLightMap = new List<Mapping>();
            var lightTempMap = new List<Mapping>();
            var tempHumidityMap = new List<Mapping>();
            var humidityLocationMap = new List<Mapping>();

            var whichMap = "";
            var incomingMapping = new Mapping();

            foreach (var line in fileData)
            {
                if (line.StartsWith("seeds:"))
                {
                    var seedData = fileData[0].Split(":")[1].Trim().Split(" ").Select(s => long.Parse(s)).ToList();
                    for (int i = 0; i < seedData.Count; i += 2)
                    {
                        seedList.Add(new InputRange { InputRangeStart = seedData[i], InputRangeEnd = seedData[i] + seedData[i + 1] - 1, InputRangeLength = seedData[i + 1] });
                    }
                    continue;
                }
                if (line == "") continue;
                if (line.EndsWith(":"))
                {
                    whichMap = line;
                    continue;
                }

                var values = line.Split(" ");
                var rangeLength = long.Parse(values[2]);
                incomingMapping.SourceRangeStart = long.Parse(values[1]);
                incomingMapping.SourceRangeEnd = incomingMapping.SourceRangeStart + rangeLength - 1;
                incomingMapping.DestinationRangeStart = long.Parse(values[0]);
                incomingMapping.DestinationRangeEnd = incomingMapping.DestinationRangeStart + rangeLength - 1;

                switch (whichMap)
                {
                    case "seed-to-soil map:":
                        seedSoilMap.Add(incomingMapping);
                        break;
                    case "soil-to-fertilizer map:":
                        soilFertilizerMap.Add(incomingMapping);
                        break;
                    case "fertilizer-to-water map:":
                        fertilizerWater.Add(incomingMapping);
                        break;
                    case "water-to-light map:":
                        waterLightMap.Add(incomingMapping);
                        break;
                    case "light-to-temperature map:":
                        lightTempMap.Add(incomingMapping);
                        break;
                    case "temperature-to-humidity map:":
                        tempHumidityMap.Add(incomingMapping);
                        break;
                    case "humidity-to-location map:":
                        humidityLocationMap.Add(incomingMapping);
                        break;
                }
                incomingMapping = new Mapping();

            }

            var fertilizer = new List<InputRange>();
            var water = new List<InputRange>();
            var light = new List<InputRange>();
            var temp = new List<InputRange>();
            var humidity = new List<InputRange>();
            var location = new List<InputRange>();


            foreach (var seed in seedList)
            {
                var soil = ApplyMappingOnRange(seedSoilMap, seed);
                foreach (var s in soil)
                {
                    fertilizer.AddRange(ApplyMappingOnRange(soilFertilizerMap, s));
                }
                foreach(var f in fertilizer)
                {
                    water.AddRange(ApplyMappingOnRange(fertilizerWater, f));
                }
                foreach(var w in water)
                {
                    light.AddRange(ApplyMappingOnRange(waterLightMap, w));
                }
                foreach(var l in light)
                {
                    temp.AddRange(ApplyMappingOnRange(lightTempMap, l));
                }
                foreach (var t in temp)
                {
                    humidity.AddRange(ApplyMappingOnRange(tempHumidityMap, t));
                }
                foreach (var h in humidity)
                {
                    location.AddRange(ApplyMappingOnRange(humidityLocationMap, h));
                }

                fertilizer = new List<InputRange>();
                water = new List<InputRange>();
                light = new List<InputRange>();
                temp = new List<InputRange>();
                humidity = new List<InputRange>();
            }

            var answer = location.Min(l => l.InputRangeStart);
            var blah = location.Where(l => l.InputRangeStart == 0);

            Console.WriteLine(answer);
        }

        [Test]
        public void Part2SLOW()
        {
            //Parse the input
            var seedList = new List<InputRange>();

            var seedSoilMap = new List<Mapping>();
            var soilFertilizerMap = new List<Mapping>();
            var fertilizerWater = new List<Mapping>();
            var waterLightMap = new List<Mapping>();
            var lightTempMap = new List<Mapping>();
            var tempHumidityMap = new List<Mapping>();
            var humidityLocationMap = new List<Mapping>();

            var whichMap = "";
            var incomingMapping = new Mapping();

            foreach (var line in fileData)
            {
                if (line.StartsWith("seeds:"))
                {
                    var seedData = fileData[0].Split(":")[1].Trim().Split(" ").Select(s => long.Parse(s)).ToList();
                    for (int i = 0; i < seedData.Count; i += 2)
                    {
                        seedList.Add(new InputRange { InputRangeStart = seedData[i], InputRangeEnd = seedData[i] + seedData[i + 1] - 1, InputRangeLength = seedData[i + 1] });
                    }
                    continue;
                }
                if (line == "") continue;
                if (line.EndsWith(":"))
                {
                    whichMap = line;
                    continue;
                }

                var values = line.Split(" ");
                var rangeLength = long.Parse(values[2]);
                incomingMapping.SourceRangeStart = long.Parse(values[1]);
                incomingMapping.SourceRangeEnd = incomingMapping.SourceRangeStart + rangeLength - 1;
                incomingMapping.DestinationRangeStart = long.Parse(values[0]);
                incomingMapping.DestinationRangeEnd = incomingMapping.DestinationRangeStart + rangeLength - 1;

                switch (whichMap)
                {
                    case "seed-to-soil map:":
                        seedSoilMap.Add(incomingMapping);
                        break;
                    case "soil-to-fertilizer map:":
                        soilFertilizerMap.Add(incomingMapping);
                        break;
                    case "fertilizer-to-water map:":
                        fertilizerWater.Add(incomingMapping);
                        break;
                    case "water-to-light map:":
                        waterLightMap.Add(incomingMapping);
                        break;
                    case "light-to-temperature map:":
                        lightTempMap.Add(incomingMapping);
                        break;
                    case "temperature-to-humidity map:":
                        tempHumidityMap.Add(incomingMapping);
                        break;
                    case "humidity-to-location map:":
                        humidityLocationMap.Add(incomingMapping);
                        break;
                }
                incomingMapping = new Mapping();

            }

            var shortestLocation = long.MaxValue;
            foreach (var seed in seedList)
            {
                for (long s = seed.InputRangeStart; s <= seed.InputRangeEnd; s++)
                {
                    var soil = ApplyMapping(seedSoilMap, s);
                    var fertilizer = ApplyMapping(soilFertilizerMap, soil);
                    var water = ApplyMapping(fertilizerWater, fertilizer);
                    var light = ApplyMapping(waterLightMap, water);
                    var temp = ApplyMapping(lightTempMap, light);
                    var humidity = ApplyMapping(tempHumidityMap, temp);
                    var location = ApplyMapping(humidityLocationMap, humidity);

                    if (location < shortestLocation) shortestLocation = location;
                }
            }
            Console.WriteLine(shortestLocation);
        }

        public long ApplyMapping(List<Mapping> map, long sourceNumber)
        {
            long destination = sourceNumber;
            foreach (var mapping in map)
            {
                if (sourceNumber.IsBetween(mapping.SourceRangeStart, mapping.SourceRangeEnd))
                {
                    var offset = sourceNumber - mapping.SourceRangeStart;
                    destination = mapping.DestinationRangeStart + offset;
                    return destination;
                }
            }
            return destination;
        }

        public List<InputRange> ApplyMappingOnRange(List<Mapping> map, InputRange range)
        {
            var outputRange = new List<InputRange>();
            var remainder = range;
            foreach (var mapping in map)
            {
                if (remainder.InputRangeStart.IsBetween(mapping.SourceRangeStart, mapping.SourceRangeEnd) && remainder.InputRangeEnd.IsBetween(mapping.SourceRangeStart,mapping.SourceRangeEnd))
                {
                    var offset = remainder.InputRangeStart - mapping.SourceRangeStart;
                    outputRange.Add(new InputRange
                    {
                        InputRangeStart = mapping.DestinationRangeStart + offset,
                        InputRangeEnd = mapping.DestinationRangeStart + offset + remainder.InputRangeLength - 1,
                        InputRangeLength = range.InputRangeLength
                    });
                    continue;
                } 
                if (remainder.InputRangeStart.IsBetween(mapping.SourceRangeStart, mapping.SourceRangeEnd) && !remainder.InputRangeEnd.IsBetween(mapping.SourceRangeStart, mapping.SourceRangeEnd))
                {
                    var offset = remainder.InputRangeStart - mapping.SourceRangeStart; // + 1;
                    outputRange.Add(new InputRange
                    {
                        InputRangeStart = mapping.DestinationRangeStart + offset,
                        InputRangeEnd = mapping.DestinationRangeEnd,
                        InputRangeLength = mapping.DestinationRangeEnd - (mapping.DestinationRangeStart + offset)
                    });
                    remainder = new InputRange 
                    {
                        InputRangeStart = mapping.SourceRangeEnd + 1,
                        InputRangeEnd = mapping.SourceRangeEnd + remainder.InputRangeLength - (mapping.DestinationRangeEnd - (mapping.DestinationRangeStart + offset)),
                        InputRangeLength = remainder.InputRangeLength - (mapping.DestinationRangeEnd - (mapping.DestinationRangeStart + offset))
                    };
                    continue;
                }
                if (!remainder.InputRangeStart.IsBetween(mapping.SourceRangeStart, mapping.SourceRangeEnd) && remainder.InputRangeEnd.IsBetween(mapping.SourceRangeStart, mapping.SourceRangeEnd))
                {
                    var offset = remainder.InputRangeEnd - mapping.SourceRangeStart;
                    outputRange.Add(new InputRange
                    {
                        InputRangeStart = mapping.DestinationRangeStart,
                        InputRangeEnd = mapping.DestinationRangeStart + offset,
                        InputRangeLength = offset + 1
                    });
                    remainder = new InputRange
                    {
                        InputRangeStart = remainder.InputRangeStart,
                        InputRangeEnd = mapping.SourceRangeStart-1,
                        InputRangeLength = mapping.SourceRangeStart - remainder.InputRangeStart
                    };
                    continue;
                }
            }

            if (outputRange.Sum(r => r.InputRangeLength) != range.InputRangeLength)
            {
                outputRange.Add(remainder);
            }
            return outputRange;
        }

        public class Mapping 
        {
            public long SourceRangeStart { get; set; }
            public long SourceRangeEnd { get; set; }
            public long DestinationRangeStart { get; set; }
            public long DestinationRangeEnd { get; set; }

        }

        public class InputRange
        {
            public long InputRangeStart { get; set; }
            public long InputRangeEnd { get; set; }
            public long InputRangeLength { get; set; }
        }


    }
}
