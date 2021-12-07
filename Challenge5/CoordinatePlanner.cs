using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge5
{
    public class CoordinatePlanner
    {
        private int[,] _matrix;
        private readonly IReadOnlyCollection<Coordinate> _coordinates;
        private int _maximumX;
        private int _maximumY;

        public CoordinatePlanner(IReadOnlyCollection<Coordinate> coordinates)
        {
            _coordinates = coordinates;
            InitializeMatrix();
        }

        private void InitializeMatrix()
        {
            _maximumX = _coordinates.Max(x => x.MaximumX);
            _maximumY = _coordinates.Max(x => x.MaximumY);

            _matrix = new int[_maximumX + 1, _maximumY + 1];

            for (int row = 0; row < _maximumX; row++)
            {
                for (int column = 0; column < _maximumY; column++)
                {
                    _matrix[row, column] = 0;
                }
            }
        }

        public void ProcessCoordinates()
        {
            foreach (var coordinate in _coordinates)
            {
                if (coordinate.DirectionMove == DirectionMove.Diagonal)
                {
                    MoveDiagional(coordinate);
                }
                else
                {
                    MoveOneDirection(coordinate);
                }

            }
        }

        private void MoveDiagional(Coordinate coordinate)
        {
            var isVerticalMovementPositive = coordinate.StartPoint.Y < coordinate.EndPoint.Y;

            for (int row = coordinate.StartPoint.X; row <= coordinate.EndPoint.X; row++)
            {
                if (isVerticalMovementPositive)
                {
                    for (int column = coordinate.StartPoint.Y; column <= coordinate.EndPoint.Y; column++)
                    {
                        _matrix[row, column] += 1;
                        row++;
                    }
                }
                else
                {
                    for (int column = coordinate.StartPoint.Y; column >= coordinate.EndPoint.Y; column--)
                    {
                        _matrix[row, column] += 1;
                        row++;
                    }
                }
            }
        }

        private void MoveOneDirection(Coordinate coordinate)
        {
            for (int row = coordinate.StartPoint.X; row <= coordinate.EndPoint.X; row++)
            {
                for (int column = coordinate.StartPoint.Y; column <= coordinate.EndPoint.Y; column++)
                {
                    _matrix[row, column] += 1;
                }
            }
        }

        public void DrawMatrix()
        {
            for (int column = 0; column <= _maximumY; column++)
            {
                for (int row = 0; row <= _maximumX; row++)
                {
                    Console.Write($"{_matrix[row, column]} ");
                }
                Console.WriteLine("");
            }
        }

        public int CalculateDangerousPoints()
        {
            int count = 0;

            for (int column = 0; column <= _maximumY; column++)
            {
                for (int row = 0; row <= _maximumX; row++)
                {
                    if (_matrix[row, column] >= 2)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}