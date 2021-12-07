using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Challenge7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Specifying a file
            string demoPath = @"input_demo.txt";
            string path = "input.txt";

            // Calling the ReadAllLines() function
            string[] lines = File.ReadAllLines(path);

            // PartA(lines);
            PartB(lines);
        }

        private static void PartA(string[] lines)
        {
            var inputs = lines[0].Split(",").Select(int.Parse);

            var usedFuel = FindSweetSpot(inputs.ToList());
            Console.WriteLine($"Lowest fuelrate: {usedFuel}");
        }

        private static void PartB(string[] lines)
        {
            var inputs = lines[0].Split(",").Select(int.Parse);

            var usedFuel = FindSweetSpotExtensive(inputs.ToList());
            Console.WriteLine($"Lowest fuelrate: {usedFuel}");
        }

        private static int FindSweetSpot(IReadOnlyCollection<int> inputs)
        {
            var minPosition = inputs.Min();
            var maxPosition = inputs.Max();

            var lowestFuelRate = int.MaxValue;

            for (int i = minPosition; i <= maxPosition; i++)
            {
                var tempFuel = 0;

                foreach (var input in inputs)
                {
                    var fuel = input - i;

                    if (fuel <= -1)
                    {
                        fuel = fuel * -1;
                    }

                    tempFuel += fuel;
                }

                if (tempFuel < lowestFuelRate)
                {
                    lowestFuelRate = tempFuel;
                }
            }

            return lowestFuelRate;
        }

        private static int FindSweetSpotExtensive(IReadOnlyCollection<int> inputs)
        {
            var minPosition = inputs.Min();
            var maxPosition = inputs.Max();

            var lowestFuelRate = int.MaxValue;

            for (int i = minPosition; i <= maxPosition; i++)
            {
                var tempFuel = 0;

                foreach (var input in inputs)
                {
                    var fuel = input - i;

                    if (fuel <= -1)
                    {
                        fuel = fuel * -1;
                    }

                    tempFuel += CaclulateFuel(fuel);
                }

                if (tempFuel < lowestFuelRate)
                {
                    lowestFuelRate = tempFuel;
                }
            }

            return lowestFuelRate;
        }

        private static int CaclulateFuel(int fuel)
        {
            var totalFuel = 0;
            for (int i = 1; i <= fuel; i++)
            {
                totalFuel += i;
            }

            return totalFuel;
        }
    }
}
