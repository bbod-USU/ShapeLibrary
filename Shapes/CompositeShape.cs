using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
[assembly:InternalsVisibleTo("UnitTests")]
namespace Shapes
{
    [DataContract]
    public class CompositeShape : Shape
    {
        [DataMember]
        internal readonly List<Shape> thisShapesList = new List<Shape>();
        [DataMember]
        public override Color Fill { get; set; }
        [DataMember]
        public override Color Stroke { get; set; }
        [DataMember]
        public override List<Point> Points { get; protected set; }
        
        internal IFileIO _fileWriter;

        public CompositeShape()
        {
            Points = new List<Point>();
        }

        public void Add(Shape shape)
        {
            Stroke = Color.Black;
            Points.AddRange(shape.Points);
            if (shape.CompositeShape)
                throw new ShapeException("This shape has already been added to a composite");
            if (shape == this)
                throw new ShapeException("You cant add a shape to itself");
            thisShapesList.Add(shape);
            shape.CompositeShape = true;
        }


        public override double ComputeArea()
        {
            return thisShapesList.Sum(d => d.ComputeArea());
        }

        public override void Scale(double scaleFactor)
        {
            foreach (var shape in thisShapesList) shape.Scale(scaleFactor);
        }

        public override void Save(Stream stream)
        {
            _fileWriter.SaveShape(stream, this);
        }

        [ExcludeFromCodeCoverage]
        public override void Draw(Stream stream)
        {
            var tmp = new Bitmap(1000, 1000);
            var blackPen = new Pen(Stroke, 3);
            // Draw line to screen.
            using (var graphics = Graphics.FromImage(tmp))
            {
                foreach (var shape in thisShapesList)
                    for (var i = 1; i < shape.Points.Count; i++)
                        graphics.DrawLine(blackPen, (float) shape.Points[i - 1].X, (float) shape.Points[i - 1].Y,
                            (float) shape.Points[i].X,
                            (float) shape.Points[i].Y);
            }

            tmp.Save(stream, ImageFormat.Jpeg);
        }


        public void RemoveShape(Shape shape)
        {
            if(Points.Count > 0)
            {
                foreach (var point in shape.Points)
                {
                    Points.Remove(point);
                }
            }
            if (!thisShapesList.Contains(shape))
                throw new ShapeException($"{shape.GetType().Name} is not part of the composite shape.");
            thisShapesList.Remove(shape);
            shape.CompositeShape = false;
        }

        public void RemoveAllShapes()
        {
            while (thisShapesList.Count > 0)
            {
                thisShapesList[0].CompositeShape = false;
                RemoveShape(thisShapesList[0]);
            }
        }
    }
}