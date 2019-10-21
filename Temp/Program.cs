using System;
using System.Drawing;
using Shapes;
using Point = Shapes.Point;
using Rectangle = Shapes.Rectangle;

namespace Temp
{
    class Program
    {
        static void Main(string[] args)
        {
            var circle = new Circle(new Point(20, 20), 4);
            Console.WriteLine("Hello World!");
            var line = new Line(3, 4, 4, 5);
            var tmp = line.ComputeLength();
            var point = new Point(3, 3);
            point.Color = Color.Aqua;
            var rectangle = new Rectangle(new Point(3,3), new Point(7,3), new Point(7,6), new Point(3,6));
            //var rectangle2 = new Rectangle(new Point(3,0), new Point(3,0), new Point(3,2), new Point(0,3));
            Console.WriteLine($"({rectangle.Points[0].X}, {rectangle.Points[0].Y}), ({rectangle.Points[1].X}, {rectangle.Points[1].Y}), ({rectangle.Points[2].X}, {rectangle.Points[2].Y}), ({rectangle.Points[3].X}, {rectangle.Points[3].Y}) Height:{rectangle.CalculateHeight()} Width:{rectangle.CalculateWidth()} Center: {rectangle.CenterPoint.X}, {rectangle.CenterPoint.Y}");
            rectangle.Rotate(180);
            Console.WriteLine($"({rectangle.Points[0].X}, {rectangle.Points[0].Y}), ({rectangle.Points[1].X}, {rectangle.Points[1].Y}), ({rectangle.Points[2].X}, {rectangle.Points[2].Y}), ({rectangle.Points[3].X}, {rectangle.Points[3].Y}) Height:{rectangle.CalculateHeight()} Width:{rectangle.CalculateWidth()} Center: {rectangle.CenterPoint.X}, {rectangle.CenterPoint.Y}");
            rectangle.Rotate(180);
            Console.WriteLine($"({rectangle.Points[0].X}, {rectangle.Points[0].Y}), ({rectangle.Points[1].X}, {rectangle.Points[1].Y}), ({rectangle.Points[2].X}, {rectangle.Points[2].Y}), ({rectangle.Points[3].X}, {rectangle.Points[3].Y}) Height:{rectangle.CalculateHeight()} Width:{rectangle.CalculateWidth()} Center: {rectangle.CenterPoint.X}, {rectangle.CenterPoint.Y}");

        }
    }
}