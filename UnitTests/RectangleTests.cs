using NUnit.Framework;
using Moq;
using Shapes;

namespace Tests
{
    public class RectangleTests
    {
        private Mock<Point> point1;
        [SetUp]
        public void Setup()
        {
            point1 = new Mock<Point>();

        }

        [Test]
        public void RectangleWithPoints()
        {
            var rectangle = new Rectangle(
                new Point(20,20), 
                new Point(30,20),
                new Point(30,30),
                new Point(20,30));
            Assert.IsTrue(rectangle.GetType() == typeof(Rectangle));
            Assert.IsTrue(rectangle.Height == 10 && rectangle.Width == 10 );
            Assert.IsTrue(rectangle.Lines.Count == 4);
            foreach (var line in rectangle.Lines)
            {
                Assert.IsTrue(line.ComputeLength() == 10);
            }
        }
    }
}