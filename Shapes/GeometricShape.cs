using System;
using System.Runtime.Serialization;

namespace Shapes
{
    [DataContract]
    [KnownType(typeof(Circle))]
    [KnownType(typeof(Rectangle))]
    [KnownType(typeof(Triangle))]
    public abstract class GeometricShape : Shape
    {
        [DataMember] public abstract double Width { get; internal set; }

        [DataMember] public abstract double Height { get; internal set; }

        [DataMember] public abstract Point CenterPoint { get; protected set; }

        internal abstract void ComputeCenter();

        public void Rotate(double degrees)
        {
            var radians = degrees * (Math.PI / 180);
            var cosTheta = Math.Cos(radians);
            var sinTheta = Math.Sin(radians);
            foreach (var point in Points)
            {
                var oldPoint = point;


                var tempX = oldPoint.X - CenterPoint.X;
                var tempY = oldPoint.Y - CenterPoint.X;

                var rotatedX = tempX * cosTheta - tempY * sinTheta;
                var rotatedY = tempX * sinTheta + tempY * cosTheta;

                point.X = rotatedX + CenterPoint.X;
                point.Y = rotatedY + CenterPoint.Y;
            }
        }

        public void Move(double deltaX, double deltaY)
        {
            var tmpCenter = CenterPoint;

            var property = GetType().GetProperties().Equals(nameof(Points));
            Console.WriteLine(property.GetType());

            if (property)
            {
                Console.WriteLine(property.GetType());
                foreach (var point in Points)
                {
                    var tmpPoint = point - tmpCenter;
                    Console.WriteLine($"({tmpPoint.X}, {tmpPoint.Y})");
                    point.X = deltaX + tmpPoint.X;
                    point.Y = deltaY + tmpPoint.Y;
                }
            }

            CenterPoint = new Point(deltaX, deltaY);
        }
    }
}