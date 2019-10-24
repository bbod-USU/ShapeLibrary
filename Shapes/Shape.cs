using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shapes
{
    public abstract class Shape
    {
        
        public abstract double Width { get; }
        public abstract double Height { get; }
        public abstract double ComputeArea();
        
        public abstract Point CenterPoint { get; }
        
        public virtual List<Point> Points { get; }


        public abstract void Scale(double scaleFactor);
        
        
        public void Rotate(double degrees)
        {
            double radians = degrees * (Math.PI / 180);
            double cosTheta = Math.Cos(radians);
            double sinTheta = Math.Sin(radians);
            foreach (var point in Points)
            {
                var oldPoint = point;


                var tempX = oldPoint.X - CenterPoint.X;
                var tempY = oldPoint.Y - CenterPoint.X;

                var rotatedX = tempX*cosTheta - tempY*sinTheta;
                var rotatedY = tempX*sinTheta + tempY*cosTheta;

                point.X = rotatedX + CenterPoint.X;
                point.Y = rotatedY + CenterPoint.Y;

            }
        }


    }
}