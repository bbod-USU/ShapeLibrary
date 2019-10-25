using System;
using NUnit.Framework;
using Shapes;

namespace Tests
{
    public class LineTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void LinePtoP()
        {
            var point1 = new Point(1, 0);
            var point2 = new Point(3, 0);
            var line = new Line(point1, point2);
            Assert.AreEqual(point1, line.Point1);
            Assert.AreEqual(point2, line.Point2);
        }

        [Test]
        public void InvalidLinePtoP()
        {
            Point p = null;
            Assert.That(() => new Line(p, p), Throws.TypeOf<ShapeException>()
                .With.Message.EqualTo("Invalid point"));
        }

        [Test]
        public void LineXY()
        {
            var point1x = 1;
            var point1y = 0;
            var point2x = 3;
            var point2y = 0;

            var line = new Line(point1x, point1y, point2x, point2y);
            Assert.AreEqual(point1x, line.Point1.X);
            Assert.AreEqual(point1y, line.Point1.Y);
            Assert.AreEqual(point2x, line.Point2.X);
            Assert.AreEqual(point2y, line.Point2.Y);

        }

        [Test]
        public void ComputeSlopeTest()
        {
            Assert.AreEqual(1, new Line(new Point(0,0), new Point(1,1)).ComputeSlope());
        }
        
        [Test]
        public void ComputeLengthTest()
        {
            Assert.AreEqual(1, new Line(new Point(0,0), new Point(1,0)).ComputeLength());
        }

        [Test]
        public void MoveTest()
        {
            var line = new Line(new Point(0, 0), new Point(0, 1));
            line.Move(20,20);
            Assert.AreEqual(20, line.Point1.X);
            Assert.AreEqual(20, line.Point1.Y);
            Assert.AreEqual(20, line.Point2.X);
            Assert.AreEqual(21, line.Point2.Y);

        }
        
        
    }
}