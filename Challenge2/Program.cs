using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Challenge2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Specifying a file
            string path = @"input.txt";

            // Calling the ReadAllLines() function
            string[] lines = File.ReadAllLines(path);
            PartA(lines);
            PartB(lines);
        }

        private static void PartB(string[] lines)
        {
            var submarine = new SubmarineB();
            foreach (var line in lines)
            {
                var splittedValue = line.Split(" ");
                submarine.AddLine(splittedValue[0], int.Parse(splittedValue[1]));
            }

            Console.WriteLine(submarine.FinalPosition());
            Console.Read();
        }

        private static void PartA(IEnumerable<string> lines)
        {
            var submarine = new Submarine();
            foreach (var line in lines)
            {
                var splittedLine = line.Split(" ");
                submarine.AddLine(splittedLine[0], int.Parse(splittedLine[1]));
            }

            Console.WriteLine(submarine.FinalPosition());
            Console.ReadLine();
        }

        public class Submarine
        {
            private const string Forward = "forward";
            private const string Down = "down";
            private const string Up = "up";

            private readonly Dictionary<string, List<int>> _actions;

            public Submarine()
            {
                _actions = new Dictionary<string, List<int>>
                {
                    [Forward] = new(),
                    [Down] = new(),
                    [Up] = new()
                };
            }

            public void AddLine(string action, int value)
            {
                _actions[action].Add(value);
            }

            public int FinalPosition()
            {
                var horizontalPosition = _actions[Forward].Sum();
                var verticalPosition = _actions[Down].Sum() - _actions[Up].Sum();

                return horizontalPosition * verticalPosition;
            }
        }


        public class SubmarineB
        {
            private const string Forward = "forward";
            private const string Down = "down";
            private const string Up = "up";

            private readonly List<Actions> _actions;

            public SubmarineB()
            {
                _actions = new List<Actions>();
            }

            public void AddLine(string action, int value)
            {
                _actions.Add(new Actions
                {
                    Name = action,
                    Value = value
                });
            }

            public int FinalPosition()
            {
                int aim = 0;
                int horizontalPosition = 0;
                int verticalPosition = 0;

                foreach (var action in _actions)
                {
                    switch (action.Name)
                    {
                        case Forward:
                            if (aim == 0)
                            {
                                horizontalPosition += action.Value;
                            }
                            else
                            {
                                horizontalPosition += action.Value;
                                verticalPosition += action.Value * aim;
                            }
                            break;

                        case Down:
                            aim += action.Value;
                            break;

                        case Up:
                            aim -= action.Value;
                            break;
                    }
                }

                return horizontalPosition * verticalPosition;
            }
        }
    }

    internal class Actions
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
