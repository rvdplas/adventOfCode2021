using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Challenge3
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
            PartB(lines);
        }

        private static void PartB(IReadOnlyList<string> lines)
        {
            var columns = ExtractColumnValues(lines);

            var oxygenGeneratorRating = FindOxygenGeneratorRating(lines, columns, 0, "");
            Console.WriteLine($"Oxygen Rating: {oxygenGeneratorRating}");

            var co2Rating = FindCo2ScrubingRating(lines, columns, 0, "");
            Console.WriteLine($"CO2 Rating: {co2Rating}");

            Console.WriteLine($"Result: {oxygenGeneratorRating * co2Rating}");
        }

        private static int FindCo2ScrubingRating(IReadOnlyCollection<string> lines, int[] columns, int indexColumn, string filter)
        {
            if (lines.Count == 1)
            {
                return ConvertBitTextToInt(lines.Single());
            }
            else
            {
                try
                {
                    var commonlyUsedNumber = IsLessCommonUsedNumberAOne(columns[indexColumn], lines);
                    filter += commonlyUsedNumber.ToString();
                }
                catch (InvalidOperationException)
                {
                    // don't update filter, both 0 and 1 are equally common
                }

                var filteredLines = lines.Where(x => x.StartsWith(filter)).ToList();
                columns = ExtractColumnValues(filteredLines);

                indexColumn++;
                return FindCo2ScrubingRating(filteredLines, columns, indexColumn, filter);
            }
        }

        private static int[] ExtractColumnValues(IReadOnlyList<string> lines)
        {
            var columns = new int[lines[0].Length];
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    columns[j] += int.Parse(lines[i][j].ToString());
                }
            }

            return columns;
        }

        private static int FindOxygenGeneratorRating(IReadOnlyCollection<string> lines, int[] columns, int indexColumn, string filter)
        {
            if (lines.Count == 1)
            {
                return ConvertBitTextToInt(lines.Single());
            }
            else
            {
                try
                {
                    var commonlyUsedNumber = IsCommonUsedNumberAOne(columns[indexColumn], lines);
                    filter += commonlyUsedNumber.ToString();
                }
                catch (InvalidOperationException)
                {
                    // don't update filter, both 0 and 1 are equally common
                }

                var filteredLines = lines.Where(x => x.StartsWith(filter)).ToList();
                columns = ExtractColumnValues(filteredLines);

                indexColumn++;
                return FindOxygenGeneratorRating(filteredLines, columns, indexColumn, filter);
            }
        }

        private static void PartA(IReadOnlyList<string> lines)
        {
            var columns = new int[lines[0].Length];
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    columns[j] += int.Parse(lines[i][j].ToString());
                }
            }

            var gammaText = "";
            var epsilonText = "";
            foreach (var value in columns)
            {
                var valueToAdd = IsCommonUsedNumberAOne(value,lines);

                gammaText += valueToAdd;
                epsilonText += valueToAdd * -1;
            }

            var gamma = ConvertBitTextToInt(gammaText);
            var epsilon = ConvertBitTextToInt(epsilonText);
            var power = gamma * epsilon;

            Console.WriteLine($"Gamma: {gamma}");
            Console.WriteLine($"Epsilon: {epsilon}");
            Console.WriteLine($"Power used: {power}");
        }

        private static int IsCommonUsedNumberAOne(int value, IReadOnlyCollection<string> lines)
        {
            double halfNumberOfLines = lines.Count / 2.0;

            if (halfNumberOfLines.Equals(value))
            {
                return 1;
            }

            if (value != 0 && Math.Round((decimal)(lines.Count / value)) == 1)
            {
                return 1;
            }

            return 0;
        }

        private static int IsLessCommonUsedNumberAOne(int value, IReadOnlyCollection<string> lines)
        {
            double halfNumberOfLines = lines.Count / 2.0;
            return value < halfNumberOfLines ? 1 : 0;
        }

        private static int ConvertBitTextToInt(string text)
        {
            return Convert.ToInt32(text, 2);
        }
    }
}
