using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;

namespace Challenge1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Specifying a file
            string path = @"input.txt";

            // Calling the ReadAllLines() function
            string[] readText = File.ReadAllLines(path);

            // PartA(readText);
            PartB(readText);
        }

        private static void PartB(string[] readText)
        {
            var numberOfThree = readText.Length - 2;

            var combinedValues = new List<int>();
            for (int i = 0; i <= numberOfThree; i++)
            {
                combinedValues.Add(ParseAndSumValues(readText.Skip(i).Take(3)));
            }

            var numberOfIncreases = 0;
            int? previousValue = null;
            foreach (int currentValue in combinedValues)
            {
                if (previousValue < currentValue)
                {
                    numberOfIncreases++;
                }
                previousValue = currentValue;
            }

            Console.WriteLine($"Total increases {numberOfIncreases}");
        }

        private static int ParseAndSumValues(IEnumerable<string> values)
        {
            return values.Sum(int.Parse);
        }

        private static void PartA(string[] readText)
        {
            var numberOfIncreases = 0;
            int? previousValue = null;
            foreach (string line in readText)
            {
                var currentValue = int.Parse(line);
                if (previousValue < currentValue)
                {
                    numberOfIncreases++;
                }
                previousValue = currentValue;
            }

            Console.WriteLine($"Total increases {numberOfIncreases}");
        }
    }
}
