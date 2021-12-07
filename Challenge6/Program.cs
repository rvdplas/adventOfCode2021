using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge6
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

            PartA(lines);
            // PartB(lines);
        }

        private static void PartA(string[] lines)
        {
            var fishes = new ConcurrentDictionary<int, long>
            {
                [0] = 0,
                [1] = 0,
                [2] = 0,
                [3] = 0,
                [4] = 0,
                [5] = 0,
                [6] = 0,
                [7] = 0,
                [8] = 0
            };

            foreach (var line in lines)
            {
                var splittedLine = line.Split(",").ToList();

                foreach (var value in splittedLine)
                {
                    var parsedToDays = int.Parse(value);
                    fishes[parsedToDays] += 1;
                }
            }

            DaysGoBye(fishes, 256);
        }

        private static void DaysGoBye(ConcurrentDictionary<int, long> fishes, int days)
        {
            for (int i = 0; i < days; i++)
            {
                long numberOfNewFishes = 0;

                foreach (var (key, value) in fishes)
                {
                    switch (key)
                    {
                        case 0:
                            numberOfNewFishes = value;
                            break;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                            fishes[key - 1] = value;
                            break;
                    }
                }

                fishes[6] += numberOfNewFishes;
                fishes[8] = numberOfNewFishes;
            }

            long totalFishes = 0;

            foreach (var fish in fishes)
            {
                totalFishes += fish.Value;
            }

            Console.WriteLine($"Total fishes: {totalFishes}");
        }
    }
}
