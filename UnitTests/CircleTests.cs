using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Moq;
using NUnit.Framework;
using Shapes;

namespace Tests
{
    public class CircleTests
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
        public void CircleXYTest()
        {
            var circle = new Circle(10, 10, 1);
            Assert.AreEqual(10, circle.CenterPoint.X);
            Assert.AreEqual(10, circle.CenterPoint.Y);
            Assert.AreEqual(2, circle.Height);
            Assert.AreEqual(2, circle.Height);
            Assert.AreEqual(2, circle.Width);
            Assert.AreEqual(Color.Empty, circle.Fill);
            Assert.AreEqual(Color.Black, circle.Stroke);
            circle.Fill = Color.Aqua;
            Assert.AreEqual(Color.Aqua, circle.Fill);
            Assert.IsNull(circle.Points);
            Assert.AreEqual(1, circle.Radius);
        }


        [Test]
        public void CirclePointRadiusTest()
        {
            var circle = new Circle(new Shapes.Point(10, 10), 1);
            Assert.AreEqual(10, circle.CenterPoint.X);
            Assert.AreEqual(10, circle.CenterPoint.Y);
            Assert.AreEqual(2, circle.Height);
            Assert.AreEqual(2, circle.Height);
            Assert.AreEqual(2, circle.Width);
            Assert.AreEqual(Color.Empty, circle.Fill);
            Assert.AreEqual(Color.Black, circle.Stroke);
            circle.Fill = Color.Aqua;
            Assert.AreEqual(Color.Aqua, circle.Fill);
            Assert.IsNull(circle.Points);
            Assert.AreEqual(1, circle.Radius);
        }

        [Test]
        public void InvalidCircleXYTest()
        {
            Assert.That(() => new Circle(10, 10, -1), Throws.TypeOf<ShapeException>()
                .With.Message.EqualTo("Invalid radius"));
        }

        [Test]
        public void InvalidCirclePointRadiusTest()
        {
            Assert.That(() => new Circle(new Shapes.Point(10, 10), -1), Throws.TypeOf<ShapeException>()
                .With.Message.EqualTo("Invalid radius"));
        }

        [Test]
        public void ScaleTest()
        {
            var circle = new Circle(10, 10, 1);
            circle.Scale(10);
            Assert.AreEqual(10, circle.Radius);
        }

        [Test]
        public void BadScaleTest()
        {
            var circle = new Circle(10, 10, 1);
            Assert.That(() => circle.Scale(-10), Throws.TypeOf<ShapeException>()
                .With.Message.EqualTo("Invalid scale factor"));
        }

        [Test]
        public void ComputeAreaTest()
        {
            var circle = new Circle(10, 10, 1.26);
            Assert.AreEqual(Math.PI * Math.Pow(1.26, 2), circle.ComputeArea());

        }

        [Test]
        public void SaveTest()
        {

            Shape createdObj = null;
            var circle = new Circle(
                new Shapes.Point(20, 20), 5);
            _mockFileIO.Setup(x => x.SaveShape(It.IsAny<Stream>(), It.IsAny<Shape>()))
                .Callback<Stream, Shape>((i, x) => { createdObj = x; });

            circle._fileWriter = _mockFileIO.Object;

            circle.Save(_mockFileStream.Object);
            Assert.AreEqual(circle, createdObj);

            _mockFileIO.Verify(x => x.SaveShape(It.IsAny<Stream>(),
                It.IsAny<Shape>()), Times.Once);

        }
    }
}