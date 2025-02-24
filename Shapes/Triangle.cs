using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Shapes
{
    [DataContract(Name = "Triangle", Namespace = "Shapes")]
    public class Triangle : GeometricShape
    {
        
        [DataMember]
        public sealed override List<Point> Points { get; protected set; }

        [DataMember]
        public override Color Fill { get; set; }

        [DataMember]
        public override Color Stroke { get; set; }

        [DataMember]
        public sealed override Point CenterPoint { get; protected set; }

        [DataMember]
        public List<Line> Lines { get; internal set; }
        
        internal IFileIO _fileWriter;
        
        public Triangle(Point point1, Point point2, Point point3)
        {
            Stroke = Color.Black;

            Points = new List<Point>();
            Lines = new List<Line>();

            Points.Add(point1);
            Points.Add(point2);
            Points.Add(point3);

            Lines.Add(new Line(point1, point2));
            Lines.Add(new Line(point2, point3));
            Lines.Add(new Line(point3, point1));

            ComputeCenter();
        }

        public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            Stroke = Color.Black;

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

            ComputeCenter();
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
            Validator.ValidatePositiveDouble(scaleFactor, "Invalid scale factor");

            foreach (var point in Points)
            {
                point.X *= scaleFactor;
                point.Y *= scaleFactor;
            }
        }

        public override void Save(Stream stream)
        {
            _fileWriter.SaveShape(stream, this);
        }

        [ExcludeFromCodeCoverage]
        public override void Draw(Stream stream)
        {
            var tmp = new Bitmap((int) Lines.Max(x => x.ComputeLength()) * 2, (int) Lines.Max(x => x.ComputeLength()) * 2);
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

        internal override void ComputeCenter()
        {
            var x = (Points[0].X + Points[1].X + Points[2].X) * 0.33333333333;
            var y = (Points[0].Y + Points[1].Y + Points[2].Y) * 0.33333333333;
            CenterPoint = new Point(x, y);

        }
    }
}