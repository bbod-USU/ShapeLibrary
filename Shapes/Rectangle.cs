using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;

namespace Shapes
{
    [DataContract]
    public class Rectangle : GeometricShape
    {
        public Rectangle(Point point1, Point point2, Point point3, Point point4)
        {
            Stroke = Color.Black;
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
            CenterPoint = new Point(point1.X + Width / 2, point1.Y + Height / 2);
            Validator.ValidateRectangle(Points, $"Attempted to create an invalid shape {GetType()}");
        }

        public Rectangle(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            Stroke = Color.Black;
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
            ComputeCenter();
            Validator.ValidateRectangle(Points, $"Attempted to create an invalid shape {GetType()}");
        }

        /// <inheritdoc />
        public Rectangle(Point point, Size size)
        {
            Stroke = Color.Black;
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
            ComputeCenter();
            Validator.ValidateRectangle(Points, $"Attempted to create an invalid shape {GetType()}");
        }

        [DataMember] public override List<Point> Points { get; internal set; }

        [DataMember] public override Color Fill { get; set; }

        [DataMember] public override Color Stroke { get; set; }

        [DataMember] public List<Line> Lines { get; }

        [DataMember] public override Point CenterPoint { get; protected set; }

        [DataMember] public sealed override double Width { get; internal set; }

        [DataMember] public sealed override double Height { get; internal set; }

        public override double ComputeArea()
        {
            return Height * Width;
        }

        public override void Scale(double scaleFactor)
        {
            foreach (var point in Points)
            {
                point.X *= scaleFactor;
                point.Y *= scaleFactor;
            }

            foreach (var line in Lines)
            {
                line.Point1.X *= scaleFactor;
                line.Point1.Y *= scaleFactor;
                line.Point2.X *= scaleFactor;
                line.Point2.Y *= scaleFactor;
            }
        }

        public override void Save(Stream stream)
        {
            var fileWriter = new FileIO();
            fileWriter.SaveShape(stream, this);
        }

        public override void Draw(Stream stream)
        {
            var tmp = new Bitmap((int) Width * 2, (int) Height * 2);
            var blackPen = new Pen(Stroke, 3);
            // Draw line to screen.
            using (var graphics = Graphics.FromImage(tmp))
            {
                for (var i = 1; i < Points.Count; i++)
                    graphics.DrawLine(blackPen,
                        (float) Points[i - 1].X,
                        (float) Points[i - 1].Y,
                        (float) Points[i].X,
                        (float) Points[i].Y);
            }

            tmp.Save(stream, ImageFormat.Jpeg);
        }

        public double CalculateWidth()
        {
            return new Line(Points[0], Points[3]).ComputeLength();
        }

        public double CalculateHeight()
        {
            return new Line(Points[0], Points[1]).ComputeLength();
        }

        internal override void ComputeCenter()
        {
            CenterPoint = new Point((Points[0].X + Width) / 2, (Points[0].Y + Height) / 2);
        }
    }
}