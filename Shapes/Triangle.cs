using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Shapes
{
    public class Triangle : Shape
    {
        public override Color Fill { get; set; }
        public override Color Color { get; set; }
        public override double Width { get; }
        public override double Height { get; }

        public Triangle(Point point1, Point point2, Point point3)
        {
            
        }

        public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            
        }
        
        public override double ComputeArea()
        {
            throw new NotImplementedException();
        }

        public override void Scale(double scaleFactor)
        {
            throw new NotImplementedException();
        }
    }
}