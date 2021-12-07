using System;
using System.IO;

namespace Challenge4
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
            var bingoGame = new BingoGame(lines);
            var winningValue = bingoGame.FindLastWinningBoard();

            Console.WriteLine($"Winning value: {winningValue}");
        }

        private static void PartA(string[] lines)
        {
            var bingoGame = new BingoGame(lines);
            var winningValue = bingoGame.DrawNumbers();

            Console.WriteLine($"Winning value: {winningValue}");
        }
    }
}
