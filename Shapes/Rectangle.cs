using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shapes
{
    public class Rectangle : Shape
    {
        public override List<Point> Points { get; }
        
        public List<Line> Lines { get; }
        public override Point CenterPoint { get; }
        public sealed override double Width { get;  }
        public sealed override double Height { get; }


        public Rectangle(Point point1, Point point2, Point point3, Point point4)
        {
            Points = new List<Point>();
            Lines = new List<Line>();
            
            Points.Add(point1);
            Points.Add(point2);
            Points.Add(point3);
            Points.Add(point4);
            
            Lines.Add(new Line(point1, point2));
            Lines.Add(new Line(point2, point3));
            Lines.Add(new Line(point3, point4));
            Lines.Add(new Line(point4, point1));


            Height = new Line(point1, point4).ComputeLength();
            Width = new Line(point1, point2).ComputeLength();
            CenterPoint = new Point(point1.X + (Width/2), point1.Y + (Height/2));
            Validator.ValidateRectangle(Points, $"Attempted to create an invalid shape {this.GetType()}");
        }

        public Rectangle(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            Points = new List<Point>();
            var point1 = new Point(x1, y1);
            var point2 = new Point(x2, y2);
            var point3 = new Point(x3, y3);
            var point4 = new Point(x4, y4);
            Points.Add(point1);
            Points.Add(point2);
            Points.Add(point3);
            Points.Add(point4);
            Height = new Line(point1, point2).ComputeLength();
            Width = new Line(point1, point4).ComputeLength();
            CenterPoint = new Point((point1.X + Width)/2, (point1.Y + Height)/2);
            Validator.ValidateRectangle(Points, $"Attempted to create an invalid shape {this.GetType()}");
        }

        public Rectangle(Point point, Size size)
        {
            Points = new List<Point>();
            var point1 = point;
            var point2 = new Point(point.X + size.Width, point.Y);
            var point3 = new Point(point.X + size.Width, point.Y + size.Height);
            var point4 = new Point(point.X, point.Y + size.Height);
            Points.Add(point1);
            Points.Add(point2);
            Points.Add(point3);
            Points.Add(point4);
            Height = new Line(point1, point2).ComputeLength();
            Width = new Line(point1, point4).ComputeLength();
            CenterPoint = new Point((point1.X + Width)/2, (point1.Y + Height)/2);
            Validator.ValidateRectangle(Points, $"Attempted to create an invalid shape {this.GetType()}");
        }
        
        public override double ComputeArea()
        {
            return Height * Width;
        }

        public override void Scale(double scaleFactor)
        {
            throw new System.NotImplementedException();
        }

        public double CalculateWidth()
        {
            return new Line(Points[0], Points[3]).ComputeLength();

        }

        public double CalculateHeight()
        {
            return new Line(Points[0], Points[1]).ComputeLength();
        }

        
        

    }
}