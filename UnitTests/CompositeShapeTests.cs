using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection.Metadata;
using Moq;
using NUnit.Framework;
using Shapes;
using Point = Shapes.Point;
using Rectangle = Shapes.Rectangle;

namespace Tests
{
    public class CompositeShapeTests
    {
        private Mock<Stream> _mockFileStream;
        private Mock<IFileIO> _mockFileIO;
        
        [SetUp]
        public void SetUp()
        {
            _mockFileIO = new Mock<IFileIO>();
            _mockFileStream = new Mock<Stream>();
        }

        [Test]
        public void CompositeShape()
        {
            var points = new List<Point>();
            var composite = new CompositeShape();
            var rpoint1 = new Point(20, 20);
            var rpoint2 = new Point(30, 20);
            var rpoint3 = new Point(30, 30);
            var rpoint4 = new Point(20, 30);
            
            var rect = new Rectangle(rpoint1, rpoint2, rpoint3, rpoint4);
            
            var tpoint1 = new Point(0, 0);
            var tpoint2 = new Point(3, 0);
            var tpoint3 = new Point(0, 3);
            var triangle = new Triangle(tpoint1, tpoint2, tpoint3);
            
            points.Add(rpoint1);
            points.Add(rpoint2);
            points.Add(rpoint3);
            points.Add(rpoint4);
            points.Add(tpoint1);
            points.Add(tpoint2);
            points.Add(tpoint3);

            var area = rect.ComputeArea() + triangle.ComputeArea();
            
            composite.Add(rect);
            composite.Add(triangle);
            
            Assert.AreEqual(area, composite.ComputeArea());
            Assert.AreEqual(Color.Empty, composite.Fill);
            Assert.AreEqual(Color.Black, composite.Stroke);
            composite.Fill = Color.Aqua;
            Assert.AreEqual(Color.Aqua, composite.Fill);

            

            foreach (var point in composite.Points)
            {    
                Assert.Contains(point, points);
            }
            
        }

        [Test]
        public void RemoveShapeTest()
        {
            var points = new List<Point>();
            var composite = new CompositeShape();
            var rpoint1 = new Point(20, 20);
            var rpoint2 = new Point(30, 20);
            var rpoint3 = new Point(30, 30);
            var rpoint4 = new Point(20, 30);
            
            var rect = new Rectangle(rpoint1, rpoint2, rpoint3, rpoint4);
            
            var tpoint1 = new Point(0, 0);
            var tpoint2 = new Point(3, 0);
            var tpoint3 = new Point(0, 3);
            var triangle = new Triangle(tpoint1, tpoint2, tpoint3);
            
            points.Add(rpoint1);
            points.Add(rpoint2);
            points.Add(rpoint3);
            points.Add(rpoint4);

            
            var area = rect.ComputeArea() + triangle.ComputeArea();
            
            composite.Add(rect);
            composite.Add(triangle);
            
            Assert.AreEqual(area, composite.ComputeArea());
           
            composite.RemoveShape(triangle);
            
            foreach (var point in composite.Points)
            {    
                Assert.Contains(point, points);
            }
            Assert.AreEqual(rect.ComputeArea(), composite.ComputeArea());
        }

        [Test]
        public void RemoveShapeNotInComposite()
        {
            var composite = new CompositeShape();
            var circle = new Circle(20,20,5);
            Assert.That(()=> composite.RemoveShape(circle), Throws.TypeOf<ShapeException>()
                .With.Message.EqualTo($"{typeof(Circle).Name} is not part of the composite shape."));
        }

        [Test]
        public void AddShapeTwicetest()
        {
            var c = new CompositeShape();
            var rpoint1 = new Point(20, 20);
            var rpoint2 = new Point(30, 20);
            var rpoint3 = new Point(30, 30);
            var rpoint4 = new Point(20, 30);
            var rect = new Rectangle(rpoint1, rpoint2, rpoint3, rpoint4);
            c.Add(rect);
            Assert.That(()=> c.Add(rect), Throws.TypeOf<ShapeException>()
                .With.Message.EqualTo("This shape has already been added to a composite"));
        }

        [Test]
        public void AddCompositeToSelf()
        {
            var c = new CompositeShape();
            Assert.That(()=> c.Add(c), Throws.TypeOf<ShapeException>()
                .With.Message.EqualTo("You cant add a shape to itself"));
        }

        [Test]
        public void AddShapeToTwoComposits()
        {
            var c = new CompositeShape();
            var co = new CompositeShape();
            var rpoint1 = new Point(20, 20);
            var rpoint2 = new Point(30, 20);
            var rpoint3 = new Point(30, 30);
            var rpoint4 = new Point(20, 30);
            var rect = new Rectangle(rpoint1, rpoint2, rpoint3, rpoint4);
            c.Add(rect);
            Assert.That(()=> co.Add(rect), Throws.TypeOf<ShapeException>()
                .With.Message.EqualTo("This shape has already been added to a composite"));
        }

        [Test]
        public void RemoveAllShapes()
        {
            var c = new CompositeShape();
            var rpoint1 = new Point(20, 20);
            var rpoint2 = new Point(30, 20);
            var rpoint3 = new Point(30, 30);
            var rpoint4 = new Point(20, 30);
            var rect = new Rectangle(rpoint1, rpoint2, rpoint3, rpoint4);
            c.Add(rect);
            var tpoint1 = new Point(0, 0);
            var tpoint2 = new Point(3, 0);
            var tpoint3 = new Point(0, 3);
            var triangle = new Triangle(tpoint1, tpoint2, tpoint3);
            c.Add(triangle);
            
            c.RemoveAllShapes();
            Assert.AreEqual(0,c.Points.Count);
            Assert.AreEqual(0, c.thisShapesList.Count);
        }

        [Test]
        public void ScaleTest()
        {
            var c = new CompositeShape();
            var rpoint1 = new Point(1, 1);
            var rpoint2 = new Point(2, 1);
            var rpoint3 = new Point(2, 2);
            var rpoint4 = new Point(1, 2);
            var rect = new Rectangle(rpoint1, rpoint2, rpoint3, rpoint4);
            c.Add(rect);
            var tpoint1 = new Point(0, 0);
            var tpoint2 = new Point(1, 0);
            var tpoint3 = new Point(0, 1);
            var triangle = new Triangle(tpoint1, tpoint2, tpoint3);
            c.Add(triangle);
            var area = c.ComputeArea()*2;
            c.Scale(2);
            Assert.Less(Math.Abs(c.ComputeArea()-area), .000000000000001);
        }

        [Test]
        public void SaveTest()
        {
            Shape createdObj = null;
            var c = new CompositeShape();
            _mockFileIO.Setup(x => x.SaveShape(It.IsAny<Stream>(), It.IsAny<Shape>()))
                .Callback<Stream, Shape>((i, x) => { createdObj = x; });

            c._fileWriter = _mockFileIO.Object;

            c.Save(_mockFileStream.Object);
            Assert.AreEqual(c, createdObj);

            _mockFileIO.Verify(x => x.SaveShape(It.IsAny<Stream>(),
                It.IsAny<Shape>()), Times.Once);
            
        }
    }
}