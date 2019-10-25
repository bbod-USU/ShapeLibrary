using System.Drawing;
using System.Runtime.Serialization;

namespace Shapes
{
    [DataContract(Name = "Points", Namespace = "Shapes")]
    public class Point
    {
        public Point(double x, double y)
        {
            Validator.ValidateDouble(x, "Invalid x-location point");
            Validator.ValidateDouble(y, "Invalid y-location point");
            X = x;
            Y = y;
            Color = Color.Black;
        }

        [DataMember] public double X { get; internal set; }

        [DataMember] public double Y { get; internal set; }

        [DataMember] public Color Color { get; set; }

        /**
        * Move the point in the x direction
        *
        * @param deltaX            The delta amount to move the point -- must be a valid double
        * @throws ShapeException   Exception thrown if the parameter is invalid
        */
        public void MoveX(double deltaX)
        {
            Validator.ValidateDouble(deltaX, "Invalid delta-x value");
            X += deltaX;
        }

        /**
         * Move the point in the y direction
         *
         * @param deltaY            The delta amount to move the point -- must be a valid double
         * @throws ShapeException   Exception thrown if the parameter is invalid
         */
        public void MoveY(double deltaY)
        {
            Validator.ValidateDouble(deltaY, "Invalid delta-y value");
            Y += deltaY;
        }

        /**
         * Move the point
         *
         * @param deltaX            The delta amount to move the point in the x direction -- must be a valid double
         * @param deltaY            The delta amount to move the point in the y direction -- must be a valid double
         * @throws ShapeException   Exception throw if any parameter is invalid
         */
        public void Move(double deltaX, double deltaY)
        {
            MoveX(deltaX);
            MoveY(deltaY);
        }

        /**
         * Copy the point
         * @return                  A new point with same x and y locations
         * @throws ShapeException   Should never thrown because the current x and y are valid
         */
        public Point Copy()
        {
            return new Point(X, Y);
        }

        public static Point operator -(Point point1, Point point2)
        {
            var x = point1.X - point2.X;
            var y = point1.Y - point2.Y;
            return new Point(x, y);
        }

        public static Point operator +(Point point1, Point point2)
        {
            var x = point1.X + point2.X;
            var y = point1.Y + point2.Y;
            return new Point(x, y);
        }

        public static Point operator /(Point point1, double divisor)
        {
            var x = point1.X / divisor;
            var y = point1.Y / divisor;
            return new Point(x, y);
        }
    }
}