using System.Drawing;
using NUnit.Framework;
using Shapes;

namespace Tests
{
    public class CircleTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CircleXY()
        {
            var circle = new Circle(10, 10, 1);
            Assert.AreEqual(10, circle.CenterPoint.X);
            Assert.AreEqual(10, circle.CenterPoint.Y);
            Assert.AreEqual(2, circle.Height);            Assert.AreEqual(2, circle.Height);
            Assert.AreEqual(2, circle.Width);
            Assert.AreEqual(Color.Empty, circle.Fill);
            Assert.AreEqual(Color.Black, circle.Stroke);
            Assert.IsNull(circle.Points);
            Assert.AreEqual(1, circle.Radius);
        }
    }
}