using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using Shapes;
using Image = Shapes.Image;
using Point = Shapes.Point;
using Rectangle = Shapes.Rectangle;

namespace Temp
{
    class Program
    {
        static void Main(string[] args)
        {
            var circle = new Circle(new Point(20, 20), 4);
            
            Console.WriteLine($"({circle.CenterPoint.X}, {circle.CenterPoint.Y})");
            circle.Move(5,5);
            Console.WriteLine($"({circle.CenterPoint.X}, {circle.CenterPoint.Y})");

            
            
            var line = new Line(3, 4, 4, 5);
            var tmp = line.ComputeLength();
            var point = new Point(3, 3);
            var rectangle = new Rectangle(new Point(3,3), new Point(5,3), new Point(5,5), new Point(3,5));
            //var rectangle2 = new Rectangle(new Point(3,0), new Point(3,0), new Point(3,2), new Point(0,3));
            Console.WriteLine($"({rectangle.Points[0].X}, {rectangle.Points[0].Y}), ({rectangle.Points[1].X}, {rectangle.Points[1].Y}), ({rectangle.Points[2].X}, {rectangle.Points[2].Y}), ({rectangle.Points[3].X}, {rectangle.Points[3].Y}) Height:{rectangle.CalculateHeight()} Width:{rectangle.CalculateWidth()} Center: {rectangle.CenterPoint.X}, {rectangle.CenterPoint.Y}");
//            rectangle.Rotate(180);
//            Console.WriteLine($"({rectangle.Points[0].X}, {rectangle.Points[0].Y}), ({rectangle.Points[1].X}, {rectangle.Points[1].Y}), ({rectangle.Points[2].X}, {rectangle.Points[2].Y}), ({rectangle.Points[3].X}, {rectangle.Points[3].Y}) Height:{rectangle.CalculateHeight()} Width:{rectangle.CalculateWidth()} Center: {rectangle.CenterPoint.X}, {rectangle.CenterPoint.Y}");
//            rectangle.Rotate(180);
//            Console.WriteLine($"({rectangle.Points[0].X}, {rectangle.Points[0].Y}), ({rectangle.Points[1].X}, {rectangle.Points[1].Y}), ({rectangle.Points[2].X}, {rectangle.Points[2].Y}), ({rectangle.Points[3].X}, {rectangle.Points[3].Y}) Height:{rectangle.CalculateHeight()} Width:{rectangle.CalculateWidth()} Center: {rectangle.CenterPoint.X}, {rectangle.CenterPoint.Y}");
            rectangle.Move(15,10);
           // Console.WriteLine($"({rectangle.Points[50].X}, {rectangle.Points[50].Y}), ({rectangle.Points[100].X}, {rectangle.Points[50].Y}), ({rectangle.Points[100].X}, {rectangle.Points[100].Y}), ({rectangle.Points[50].X}, {rectangle.Points[100].Y}) Height:{rectangle.CalculateHeight()} Width:{rectangle.CalculateWidth()} Center: {rectangle.CenterPoint.X}, {rectangle.CenterPoint.Y}");

            
            
            var triangle = new Triangle(new Point(3,3), new Point(7,3), new Point(7,6));
            Console.WriteLine($"\n \n ({triangle.Points[0].X}, {triangle.Points[0].Y}) ({triangle.Points[1].X}, {triangle.Points[1].Y}) ({triangle.Points[2].X}, {triangle.Points[2].Y}) Center: ({triangle.CenterPoint.X}, {triangle.CenterPoint.Y})  Area = {triangle.ComputeArea()} Height: {triangle.Height}");
            triangle.Rotate(30);
            Console.WriteLine($"({triangle.Points[0].X}, {triangle.Points[0].Y}) ({triangle.Points[1].X}, {triangle.Points[1].Y}) ({triangle.Points[2].X}, {triangle.Points[2].Y}) Center: ({triangle.CenterPoint.X}, {triangle.CenterPoint.Y})  Area = {triangle.ComputeArea()} Height: {triangle.Height}");
            
            
            var composite = new CompositeShape();
            composite.Add(new Rectangle(new Point(500, 500), new Size(100, 100) ));
           // composite.Add(triangle);
            var composite2 = new CompositeShape();
            composite2.Add(composite);
            FileStream save = new FileStream(@"/Users/bradybodily/Documents/testing/testing.jpg", FileMode.Create);
            composite.Draw(save);
            
            composite2.RemoveShape(composite);
            composite.RemoveAllShapes();
            
            FileStream wf = new FileStream(@"/Users/bradybodily/Documents/testing/test.txt", FileMode.Create);
            triangle.Save(wf);
            
            FileStream fs = new FileStream(@"/Users/bradybodily/Documents/testing/test.txt", FileMode.Open);
            var triangle2 = new ShapeFactory().GetShapeFromFile<Triangle>(fs);
            Console.WriteLine($"({triangle2.Points[0].X}, {triangle2.Points[0].Y}) ({triangle2.Points[1].X}, {triangle2.Points[1].Y}) ({triangle2.Points[2].X}, {triangle2.Points[2].Y}) Center: ({triangle2.CenterPoint.X}, {triangle2.CenterPoint.Y})  Area = {triangle2.ComputeArea()} Height: {triangle2.Height}");

            FileStream imf = new FileStream(@"/Users/bradybodily/Documents/testing/visual-reverse-image-search-v2_intro.jpg", FileMode.Create);

            Image i = new Image(new Point(20,20), new Size(20, 20), imf );
        }
    }
}