using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Shapes
{
    public class CompositeShape : Shape
    {
        private List<Shape> thisShapesList = new List<Shape>();
        
        public override Color Fill { get; set; }
        public override Color Stroke { get; set; }

        public void Add(Shape shape)
        {
            if(shape.CompositeShape)
                throw new ShapeException($"This shape has already been added to a composite");
            if(shape == this)
                throw new ShapeException($"You cant add a shape to itself");
            thisShapesList.Add(shape);
            shape.CompositeShape = true;
        }

        

        public override double ComputeArea()
        {
            return thisShapesList.Sum(d => d.ComputeArea());
        }

        public override void Scale(double scaleFactor)
        {
            foreach (var shape in thisShapesList)
            {
                shape.Scale(scaleFactor);
            }
        }

        public override void Save(Stream stream)
        {
            var fileWriter = new FileIO();
            fileWriter.SaveShape(stream, this); 
        }

        public override void Draw(Stream stream)
        {
            var tmp = new Bitmap(1000, 1000);
            Pen blackPen = new Pen(Stroke, 3);
            // Draw line to screen.
            using (var graphics = Graphics.FromImage(tmp))
            {
                foreach (var shape in thisShapesList)
                {
                    for (int i = 1; i < shape.Points.Count; i++)
                        graphics.DrawLine(blackPen, (float) shape.Points[i - 1].X, (float) shape.Points[i - 1].Y, (float) shape.Points[i].X,
                            (float) shape.Points[i].Y);
                }
                
            }

            tmp.Save(stream, ImageFormat.Jpeg);
        }


        public void RemoveShape(Shape shape)
        {
            if(!thisShapesList.Contains(shape))
                throw new ShapeException($"{shape.GetType().Name} is not part of the composite shape.");
            thisShapesList.Remove(shape);
            shape.CompositeShape = false;
            
        }

        public void RemoveAllShapes()
        {
            while (thisShapesList.Count > 0)
            {
                thisShapesList[0].CompositeShape = false;
                thisShapesList.Remove(thisShapesList[0]);
            }
        }

    }
}