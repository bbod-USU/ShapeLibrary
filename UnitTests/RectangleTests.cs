using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using NUnit.Framework;
using Moq;
using NUnit.Framework.Internal;
using Shapes;
using Point = Shapes.Point;
using Rectangle = Shapes.Rectangle;

namespace Tests
{
    public class RectangleTests
    {
        private Mock<Stream> _mockFileStream;
        private Mock<IFileIO> _mockFileIO;

        [SetUp]
        public void Setup()
        {
            _mockFileIO = new Mock<IFileIO>();
            _mockFileStream = new Mock<Stream>();
        }

        [Test]
        public void RectangleWithPoints()
        {
            var rectangle = new Rectangle(
                new Point(20, 20),
                new Point(30, 20),
                new Point(30, 30),
                new Point(20, 30));
            Assert.IsTrue(rectangle.GetType() == typeof(Rectangle));
            Assert.IsTrue(rectangle.Height == 10 && rectangle.Width == 10);
            Assert.IsTrue(rectangle.Lines.Count == 4);
            Assert.IsTrue(rectangle.CenterPoint.X == 25 && rectangle.CenterPoint.Y == 25);
            Assert.AreEqual(rectangle.Fill, Color.Empty);
            Assert.AreEqual(rectangle.Stroke, Color.Black);
            Assert.AreEqual(rectangle.ComputeArea(), 100);
            Assert.AreEqual(rectangle.CalculateHeight(), 10);
            Assert.AreEqual(rectangle.CalculateWidth(), 10);
            Assert.IsFalse(rectangle.CompositeShape);
            rectangle.Fill = Color.Aqua;
            Assert.AreEqual(Color.Aqua, rectangle.Fill);
            foreach (var line in rectangle.Lines)
            {
                Assert.IsTrue(line.ComputeLength() == 10);
            }
        }

        [Test]
        public void BadRectangleWithPoints()
        {
            Assert.That(()=> new Rectangle(
                new Point(30, 20),
                new Point(30, 20),
                new Point(30, 30),
                new Point(20, 30)), Throws.TypeOf<ShapeException>()
                .With.Message.EqualTo($"Attempted to create an invalid shape {typeof(Rectangle)}"));
        }
        
        [Test]
        public void RectangleWithSinglePoint()
        {
            var rectangle = new Rectangle(new Point(20,10), new Size(10,10) );
            Assert.IsTrue(rectangle.GetType() == typeof(Rectangle));
            Assert.AreEqual(10, rectangle.Height);
            Assert.AreEqual( 10, rectangle.Width);
            Assert.IsTrue(rectangle.Lines.Count == 4);
            Assert.AreEqual(15, rectangle.CenterPoint.X);
            Assert.AreEqual(10, rectangle.CenterPoint.Y);
            Assert.AreEqual(rectangle.Fill, Color.Empty);
            Assert.AreEqual(rectangle.Stroke, Color.Black);
            Assert.AreEqual(100, rectangle.ComputeArea());
            Assert.AreEqual(10, rectangle.CalculateHeight());
            Assert.AreEqual(10, rectangle.CalculateWidth());
            Assert.IsFalse(rectangle.CompositeShape);
            rectangle.Fill = Color.Aqua;
            Assert.AreEqual(Color.Aqua, rectangle.Fill);
            foreach (var line in rectangle.Lines)
            {
                Assert.IsTrue(line.ComputeLength() == 10);
            }
        }
        
        [Test]
        public void RectangleWithXY()
        {
            var rectangle = new Rectangle(
                20, 20,
                30, 20,
                30, 30,
                20, 30);
            Assert.IsTrue(rectangle.GetType() == typeof(Rectangle));
            Assert.IsTrue(rectangle.Height == 10 && rectangle.Width == 10);
            Assert.IsTrue(rectangle.Lines.Count == 4);
            Assert.AreEqual(15, rectangle.CenterPoint.X);
            Assert.AreEqual(15, rectangle.CenterPoint.Y);            Assert.AreEqual(rectangle.Fill, Color.Empty);
            Assert.AreEqual(rectangle.Stroke, Color.Black);
            Assert.AreEqual(100, rectangle.ComputeArea());
            Assert.AreEqual(10, rectangle.CalculateHeight());
            Assert.AreEqual(10, rectangle.CalculateWidth());
            Assert.IsFalse(rectangle.CompositeShape);
            rectangle.Fill = Color.Aqua;
            Assert.AreEqual(Color.Aqua, rectangle.Fill);
            foreach (var line in rectangle.Lines)
            {
                Assert.IsTrue(line.ComputeLength() == 10);
            }
        }

        [Test]
        public void BadRectangleWithXY()
        {
            Assert.That(()=> new Rectangle(
                30, 20,
                30, 20,
                30, 30,
                20, 30), Throws.TypeOf<ShapeException>()
                .With.Message.EqualTo($"Attempted to create an invalid shape {typeof(Rectangle)}"));
        }

        [Test]
        public void SaveStreamTest()
        {
            Shape createdObj = null;
            List<Rectangle> rectangles = new List<Rectangle>();
            rectangles.Add(new Rectangle(
                new Point(20, 20),
                new Point(30, 20),
                new Point(30, 30),
                new Point(20, 30)));
            rectangles.Add(new Rectangle(
                20, 20,
                30, 20,
                30, 30,
                20, 30));
            rectangles.Add(new Rectangle(new Point(20,10), new Size(10,10)));
            
            foreach (var rectangle in rectangles)
            {
                _mockFileIO.Setup(x => x.SaveShape(It.IsAny<Stream>(), It.IsAny<Shape>()))
                    .Callback<Stream, Shape>((i, x) =>
                    {
                        createdObj = x;
                    } ); 

                rectangle._fileWriter = _mockFileIO.Object;
            
                rectangle.Save(_mockFileStream.Object);
                Assert.AreEqual(rectangle, createdObj);
            }
            _mockFileIO.Verify(x => x.SaveShape(It.IsAny<Stream>(), 
                It.IsAny<Shape>()), Times.Exactly(3));
        }

        [Test]
        public void ScaleTest()
        {
            List<Point> points = new List<Point>();

            var rectangle = new Rectangle(
               new Point(20, 20),
               new Point(30, 20),
               new Point(30, 30),
               new Point(20, 30));
            points.Add(new Point(20, 20));
            points.Add(new Point(30, 20));
            points.Add(new Point(30, 30));
            points.Add( new Point(20, 30));
            rectangle.Scale(10);
            for (int i = 0; i < rectangle.Points.Count; i++)
            {
                points[i].X *= 10; 
                points[i].Y *= 10;
                Assert.AreEqual(points[i].X, rectangle.Points[i].X);
                Assert.AreEqual(points[i].Y, rectangle.Points[i].Y);

            }
        }

        [Test]
        public void BadScaleTest()
        {
            List<Point> points = new List<Point>();

            var rectangle = new Rectangle(
                new Point(20, 20),
                new Point(30, 20),
                new Point(30, 30),
                new Point(20, 30));
            Assert.That(()=> rectangle.Scale(-20), Throws.TypeOf<ShapeException>()
                .With.Message.EqualTo("Invalid scale factor"));
        }

        [Test]
        public void MoveTest()
        {
            var rectangle = new Rectangle(
                new Point(20, 20),
                new Point(30, 20),
                new Point(30, 30),
                new Point(20, 30));
            
            rectangle.Move(40,40);
            Assert.AreEqual(35 ,rectangle.Points[0].X);
            Assert.AreEqual(35 ,rectangle.Points[0].Y);
            
            Assert.AreEqual(45 ,rectangle.Points[1].X);
            Assert.AreEqual(35 ,rectangle.Points[1].Y);
            
            Assert.AreEqual(45 ,rectangle.Points[2].X);
            Assert.AreEqual(45 ,rectangle.Points[2].Y);
            
            Assert.AreEqual(35 ,rectangle.Points[3].X);
            Assert.AreEqual(45 ,rectangle.Points[3].Y);
        }

        [Test]
        public void RotateTest()
        {
            var rectangle = new Rectangle(
                new Point(20, 20),
                new Point(30, 20),
                new Point(30, 30),
                new Point(20, 30));
            rectangle.Rotate(90);
            Assert.AreEqual(30 ,rectangle.Points[0].X);
            Assert.AreEqual(20 ,rectangle.Points[0].Y);
            
            Assert.AreEqual(30 ,rectangle.Points[1].X);
            Assert.AreEqual(30 ,rectangle.Points[1].Y);
            
            Assert.AreEqual(20 ,rectangle.Points[2].X);
            Assert.AreEqual(30 ,rectangle.Points[2].Y);
            
            Assert.AreEqual(20 ,rectangle.Points[3].X);
            Assert.AreEqual(20 ,rectangle.Points[3].Y);
        }
    }

}