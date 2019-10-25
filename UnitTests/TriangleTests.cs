using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using Shapes;
using Point = Shapes.Point;

namespace Tests
{
    public class TriangleTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TrianglePoints()
        {
            var point1 = new Point(0, 0);
            var point2 = new Point(3, 0);
            var point3 = new Point(0, 3);
            var triangle = new Triangle(point1, point2, point3);

            Assert.AreEqual(Color.Empty, triangle.Fill);
            triangle.Fill = Color.Aqua;
            Assert.AreEqual(Color.Aqua, triangle.Fill);
            Assert.AreEqual(Color.Black, triangle.Stroke);
            Assert.AreEqual(point1, triangle.Points[0]);
            Assert.AreEqual(point2, triangle.Points[1]);
            Assert.AreEqual(point3, triangle.Points[2]);
            Assert.AreEqual(point1, triangle.Lines[0].Point1);
            Assert.AreEqual(point2, triangle.Lines[0].Point2);
            Assert.AreEqual(point2, triangle.Lines[1].Point1);
            Assert.AreEqual(point3, triangle.Lines[1].Point2);
            Assert.AreEqual(point3, triangle.Lines[2].Point1);
            Assert.AreEqual(point1, triangle.Lines[2].Point2);
            Assert.AreEqual(3, triangle.Height);
            Assert.AreEqual(3, triangle.Width);

        }

        [Test]
        public void TriangleXY()
        {
            var point1x = 0;
            var point1y = 0;

            var point2x = 3;
            var point2y = 0;

            var point3x = 0;
            var point3y = 3;

            var triangle = new Triangle(point1x, point1y, point2x, point2y, point3x, point3y);

            Assert.AreEqual(Color.Empty, triangle.Fill);
            triangle.Fill = Color.Aqua;
            Assert.AreEqual(Color.Aqua, triangle.Fill);
            Assert.AreEqual(Color.Black, triangle.Stroke);
            Assert.AreEqual(point1x, triangle.Points[0].X);
            Assert.AreEqual(point2x, triangle.Points[1].X);
            Assert.AreEqual(point3x, triangle.Points[2].X);
            Assert.AreEqual(point1x, triangle.Lines[0].Point1.X);
            Assert.AreEqual(point2x, triangle.Lines[0].Point2.X);
            Assert.AreEqual(point2x, triangle.Lines[1].Point1.X);
            Assert.AreEqual(point3x, triangle.Lines[1].Point2.X);
            Assert.AreEqual(point3x, triangle.Lines[2].Point1.X);
            Assert.AreEqual(point1x, triangle.Lines[2].Point2.X);


            Assert.AreEqual(point1y, triangle.Points[0].Y);
            Assert.AreEqual(point2y, triangle.Points[1].Y);
            Assert.AreEqual(point3y, triangle.Points[2].Y);
            Assert.AreEqual(point1y, triangle.Lines[0].Point1.Y);
            Assert.AreEqual(point2y, triangle.Lines[0].Point2.Y);
            Assert.AreEqual(point2y, triangle.Lines[1].Point1.Y);
            Assert.AreEqual(point3y, triangle.Lines[1].Point2.Y);
            Assert.AreEqual(point3y, triangle.Lines[2].Point1.Y);
            Assert.AreEqual(point1y, triangle.Lines[2].Point2.Y);
            Assert.AreEqual(3, triangle.Height);
            Assert.AreEqual(3, triangle.Width);

        }

        [Test]
        public void ComputeAreaTest()
        {
            var point1 = new Point(0, 0);
            var point2 = new Point(3, 0);
            var point3 = new Point(0, 3);
            var area = 9 * .5;

            var triangle = new Triangle(point1, point2, point3);

            Assert.LessOrEqual(Math.Abs(triangle.ComputeArea() - area), 3.0E-15d);
        }

        [Test]
        public void ScaleTest()
        { 
            List<Point> points = new List<Point>();

            var triangle = new Triangle(
                new Point(0, 0),
                new Point(1, 0),
                new Point(0, 1));
                
            points.Add(new Point(0, 0));
            points.Add(new Point(1, 0));
            points.Add(new Point(0, 1));
            triangle.Scale(10);
            for (int i = 0; i < triangle.Points.Count; i++)
            {
                points[i].X *= 10; 
                points[i].Y *= 10;
                Assert.AreEqual(points[i].X, triangle.Points[i].X);
                Assert.AreEqual(points[i].Y, triangle.Points[i].Y);
            }
        }

        [Test]
        public void ComputeCenterTest()
        {
            var triangle = new Triangle(
                new Point(0, 0),
                new Point(1, 0),
                new Point(0, 1));
            Assert.AreEqual(.25, triangle.CenterPoint.X);
            Assert.AreEqual(.5, triangle.CenterPoint.Y);

        }
    }
}