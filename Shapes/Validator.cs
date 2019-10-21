using System;
using System.Collections.Generic;
using System.Globalization;

namespace Shapes
{
    public class Validator
    {

        public static void ValidateDouble(double value, string errorMessage)
        {
            if (double.IsInfinity(value) || double.IsNaN(value))
                throw new ShapeException(errorMessage);
        }

        public static void ValidatePositiveDouble(double value, String errorMessage)
        {
            ValidateDouble(value, errorMessage);
            if (value < 0)
                throw new ShapeException(errorMessage);
        }

        public static void ValidateRectangle(Point point1, Point point2, Point point3, Point point4, String errorMessage)
        {
            var TOLERANCE = Double.Epsilon + Double.Epsilon;
            var plumLine1 = new Line(point1, point3);
            var plumLine2 = new Line(point2, point4);
            var heightLine1 = new Line(point1, point4);
            var heightLine2 = new Line( point2, point3);
            var lengthLine1 = new Line(point1, point2);
            var lengthLine2 = new Line(point4, point3);
            if (Math.Abs(plumLine1.ComputeLength() - plumLine2.ComputeLength()) > TOLERANCE 
                || Math.Abs(heightLine1.ComputeLength() - heightLine2.ComputeLength()) > TOLERANCE 
                || Math.Abs(lengthLine1.ComputeLength() - lengthLine2.ComputeLength()) > TOLERANCE )
            {
                throw new ShapeException(errorMessage);
            }
            
        }



    }
}