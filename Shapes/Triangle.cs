using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;

namespace Shapes
{
    public class Triangle : Shape
    {
        public sealed override List<Point> Points { get; }

        public override double Height { get; }

        public override double Width { get; }

        public sealed override Point CenterPoint { get; }
        public List<Line> Lines { get; }

        public Triangle(Point point1, Point point2, Point point3)
        {
            Points = new List<Point>();
            Lines = new List<Line>();
            
            Points.Add(point1);
            Points.Add(point2);
            Points.Add(point3);
            
            Lines.Add(new Line(point1, point2));
            Lines.Add(new Line(point2, point3));
            Lines.Add(new Line(point3, point1));

            Point centerOfLine3 = ((point1 - point3) / 2) + point1;
            var centerTriangle = ((point1 - centerOfLine3) / 2) + point1;
            
            
            CenterPoint = centerTriangle;

            Height = new Line(point1, centerOfLine3).ComputeLength();
            Width = new Line(point1, point2).ComputeLength();


        }

        public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            var point1 = new Point(x1, y1);
            var point2 = new Point(x2, y2);
            var point3 = new Point(x3, y3);

            
            Points = new List<Point>();
            Lines = new List<Line>();
            
            Points.Add(point1);
            Points.Add(point2);
            Points.Add(point3);
            
            Lines.Add(new Line(point1, point2));
            Lines.Add(new Line(point2, point3));
            Lines.Add(new Line(point3, point1));
            
            Point centerOfLine3 = ((point1 - point3) / 2) + point1;
            var centerTriangle = ((point1 - centerOfLine3) / 2) + point1;

            CenterPoint = centerTriangle;
        }
        
        public override double ComputeArea()
        {

            var a = Lines[0].ComputeLength();
            var b = Lines[1].ComputeLength();
            var c = Lines[2].ComputeLength();

            var p = (a + b + c) / 2;

            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public override void Scale(double scaleFactor)
        {
            foreach (var point in Points)
            {
                point.X *= scaleFactor;
                point.Y *= scaleFactor;
            }
        }
    }
}