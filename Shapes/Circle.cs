using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("UnitTests")]

namespace Shapes
{
    [DataContract]
    public class Circle : GeometricShape
    {
        internal IFileIO _fileWriter;
   
        /**
         * Constructor with x-y Location for center
         *
         * @param x                 The x-location of the center of the circle -- must be a valid double
         * @param y                 The y-location of the center of the circle
         * @param radius            The radius of the circle -- must be greater or equal to zero.
         * @throws ShapeException   The exception thrown if the x, y, or z are not valid
         */
        public Circle(double x, double y, double radius)
        {
            _fileWriter = new FileIO();
            Stroke = Color.Black;
            Validator.ValidatePositiveDouble(radius, "Invalid radius");
            CenterPoint = new Point(x, y);
            Radius = radius;
            Height = radius * 2;
            Width = radius * 2;
        }

        /**
         * Constructor with a Point for center
         *
         * @param center            The x-location of the center of the circle -- must be a valid point
         * @param radius            The radius of the circle -- must be greater or equal to zero.
         * @throws ShapeException   The exception thrown if the x, y, or z are not valid
         */
        public Circle(Point center, double radius)
        {
            _fileWriter = new FileIO();
            Stroke = Color.Black;
            Validator.ValidatePositiveDouble(radius, "Invalid radius");
            CenterPoint = center;
            Radius = radius;
            Height = radius * 2;
            Width = radius * 2;
        }

        [DataMember] public override Color Fill { get; set; }

        [DataMember] public override Color Stroke { get; set; }

        [DataMember] public override Point CenterPoint { get; protected set; }

        [DataMember] public double Radius { get; private set; }

        [DataMember] public override double Height { get; internal set; }

        [DataMember] public override double Width { get; internal set; }

        [ExcludeFromCodeCoverage]
        internal override void ComputeCenter()
        {
            //Does nothing to a circle
        }


        /**
         * Scale the circle
         *
         * @param scaleFactor       a non-negative double that represents the percentage to scale the circle.
         *                          0>= and <1 to shrink.
         *                          >1 to grow.
         * @throws ShapeException   Exception thrown if the scale factor is not valid
         */
        public override void Scale(double scaleFactor)
        {
            Validator.ValidatePositiveDouble(scaleFactor, "Invalid scale factor");
            Radius *= scaleFactor;
        }

        public override void Save(Stream stream)
        {
            _fileWriter.SaveShape(stream, this);
        }

        [ExcludeFromCodeCoverage]
        public override void Draw(Stream stream)
        {
            var tmp = new Bitmap((int) Radius * 4, (int) Radius * 4);
            var blackPen = new Pen(Color.Bisque, 3);
            // Draw line to screen.
            using (var graphics = Graphics.FromImage(tmp))
            {
                graphics.DrawEllipse(blackPen, (float) CenterPoint.X, (float) CenterPoint.Y, (float) Radius * 2,
                    (float) Radius * 2);
            }

            tmp.Save(stream, ImageFormat.Jpeg);
        }

        /**
         * @return  The area of the circle.
         */
        public override double ComputeArea()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }
    }
}