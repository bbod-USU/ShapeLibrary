using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shapes;
using static Shapes.Validator;

namespace Tests
{
    public class ValidatorTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void ValidatePositiveDoubleTest()
        {
            Assert.That(()=> ValidatePositiveDouble(-10, "Error"), Throws.TypeOf<ShapeException>()
                .With.Message.EqualTo("Error"));
        }

        [Test]
        public void ValidateDoubleTest()
        {
            Assert.That(()=> ValidateDouble(Double.NaN, "Error"), Throws.TypeOf<ShapeException>()
                .With.Message.EqualTo("Error"));
        }

        [Test]
        public void ValidateRectangleTest()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(30, 30));
            points.Add(new Point(30, 30));
            points.Add(new Point(30, 30));
            points.Add(new Point(30, 30));
            
            Assert.That(()=> ValidateRectangle(points, "Error"), Throws.TypeOf<ShapeException>()
                .With.Message.EqualTo("Error"));
        }
    }
}