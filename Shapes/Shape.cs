using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;

namespace Shapes
{
    [DataContract]
    [KnownType(typeof(CompositeShape))]
    public abstract class Shape
    {
        [DataMember]
        public virtual List<Point> Points { get; internal set; }

        [DataMember]
        public abstract Color Fill { get; set; }
        [DataMember]
        public abstract Color Stroke { get; set; }
        [DataMember]
        protected internal bool CompositeShape { get; set; }

        public Shape()
        {
            CompositeShape = false;
        }

        public abstract double ComputeArea();


        public abstract void Scale(double scaleFactor);

        public abstract void Save(Stream stream);
        public abstract void Draw(Stream stream);

    }
}