using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Challenge5
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

        private static void PartB(string[] lines)
        {
            var coordinates = ExtractCoordinatesFromLines(lines);

            var planner = new CoordinatePlanner(coordinates);
            planner.ProcessCoordinates();
            // planner.DrawMatrix();

            Console.WriteLine("");
            Console.WriteLine($"Dangerous points: {planner.CalculateDangerousPoints()}");

            Console.ReadLine();
        }

        private static void PartA(string[] lines)
        {
            var coordinates = ExtractCoordinatesFromLines(lines);

            var planner = new CoordinatePlanner(coordinates.Where(x => x.DirectionMove != DirectionMove.Diagonal).ToList());
            planner.ProcessCoordinates();
            // planner.DrawMatrix();

            Console.WriteLine("");
            Console.WriteLine($"Dangerous points: {planner.CalculateDangerousPoints()}");

            Console.ReadLine();
        }

        private static List<Coordinate> ExtractCoordinatesFromLines(IEnumerable<string> lines)
        {
            return lines.Select(line => new Coordinate(line)).ToList();
        }
    }
}
