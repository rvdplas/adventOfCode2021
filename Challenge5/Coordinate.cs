using System;
using System.Drawing;

namespace Challenge5
{
    public class Coordinate
    {
        private const string SplitString = " -> ";

        public Point StartPoint { get; private set; }
        public Point EndPoint { get; private set; }

        public int MaximumX { get; }
        public int MaximumY { get; }
        public DirectionMove DirectionMove { get; }

        public Coordinate(string lineToSplit)
        {
            var splittedValue = lineToSplit.Split(SplitString);

            var startPointSplit = splittedValue[0].Split(",");
            StartPoint = new Point(int.Parse(startPointSplit[0]), int.Parse(startPointSplit[1]));

            var endPointSplit = splittedValue[1].Split(",");
            EndPoint = new Point(int.Parse(endPointSplit[0]), int.Parse(endPointSplit[1]));

            MaximumX = StartPoint.X > EndPoint.X ? StartPoint.X : EndPoint.X;
            MaximumY = StartPoint.Y > EndPoint.Y ? StartPoint.Y : EndPoint.Y;

            DirectionMove = GetDirectionMovement(StartPoint, EndPoint);

            SwapPositionIfNeeded();
        }

        private DirectionMove GetDirectionMovement(Point startPoint, Point endPoint)
        {
            if (StartPoint.X != EndPoint.X && StartPoint.Y != EndPoint.Y)
            {
                return DirectionMove.Diagonal;
            }

            return StartPoint.X == EndPoint.X ? DirectionMove.Vertical : DirectionMove.Horizontal;
        }

        private void SwapPositionIfNeeded()
        {
            switch (DirectionMove)
            {
                case DirectionMove.Diagonal when StartPoint.X > EndPoint.X:
                case DirectionMove.Horizontal when StartPoint.X > EndPoint.X:
                case DirectionMove.Vertical when StartPoint.Y > EndPoint.Y:
                    SwapPoints();
                    break;

                default:
                    // do nothing;
                    break;
            }
        }

        private void SwapPoints()
        {
            (EndPoint, StartPoint) = (StartPoint, EndPoint);
        }
    }
}