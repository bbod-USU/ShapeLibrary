using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace Shapes
{
    public abstract class Shape
    {
        public abstract Color Fill { get; set; }
        public abstract Color Color { get; set; }
        public abstract double Width { get; }
        public abstract double Height { get; }
        public abstract double ComputeArea();

        public abstract void Scale(double scaleFactor);


    }
}