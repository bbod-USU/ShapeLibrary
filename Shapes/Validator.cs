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

        public static void ValidateRectangle(List<Point> points, String errorMessage)
        { 
            List<Point> pointList = new List<Point>(points);
           
            var TOLERANCE = Double.Epsilon + Double.Epsilon;
            var plumLine1 = new Line(points[0], points[2]);
            var plumLine2 = new Line(points[1], points[3]);
            var heightLine1 = new Line(points[0], points[3]);
            var heightLine2 = new Line( points[1], points[2]);
            var lengthLine1 = new Line(points[0], points[1]);
            var lengthLine2 = new Line(points[3], points[2]);
            if (Math.Abs(plumLine1.ComputeLength() - plumLine2.ComputeLength()) > TOLERANCE 
                || Math.Abs(heightLine1.ComputeLength() - heightLine2.ComputeLength()) > TOLERANCE 
                || Math.Abs(lengthLine1.ComputeLength() - lengthLine2.ComputeLength()) > TOLERANCE )
            {
                throw new ShapeException(errorMessage);
            }

            while(pointList.Count > 0)
            {
                var tmp = pointList[0];
                pointList.Remove(tmp);
                if (pointList.Contains(tmp))
                {
                    throw new ShapeException(errorMessage);
                }
            }
            
        }



    }
}