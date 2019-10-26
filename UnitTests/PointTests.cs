using System.Drawing;
using NUnit.Framework;
using Point = Shapes.Point;

namespace Tests
{
    public class PointTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void ColorTests()
        {
            var point = new Point(10,10);
            Assert.AreEqual(Color.Black, point.Color);
            point.Color = Color.Aqua;
            Assert.AreEqual(Color.Aqua, point.Color);
        }

        [Test]
        public void MoveTest()
        {
            var point = new Point(10, 10);
            point.Move(20, 20);
            Assert.AreEqual(point.X, 30);
            Assert.AreEqual(point.Y, 30);
        }

        [Test]
        public void CopyTest()
        {
            var point = new Point(20, 20);
            Assert.AreEqual(point.Color, point.Copy().Color);
            Assert.AreEqual(point.X, point.Copy().X);
            Assert.AreEqual(point.Y, point.Copy().Y);

        }

        [Test]
        public void SubtractionTest()
        {
            var point1 = new Point(5, 5);
            var point2 = new Point(15, 15);
            var point3 = point2 - point1;
            Assert.AreEqual(point3.X, 10);
            Assert.AreEqual(point3.Y, 10);

        }
        [Test]
        public void AdditionTest()
        {
            var point1 = new Point(5, 5);
            var point2 = new Point(15, 15);
            var point3 = point2 + point1;
            Assert.AreEqual(20, point3.X);
            Assert.AreEqual(20, point3.Y);

        }
        [Test]
        public void DivisionTest()
        {
            var point1 = new Point(10, 10);
            var point3 = point1 / 2;
            Assert.AreEqual(point3.X, 5);
            Assert.AreEqual(point3.Y, 5);

        }
    }
}