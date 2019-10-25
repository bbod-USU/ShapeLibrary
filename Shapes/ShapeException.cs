using System;

namespace Shapes
{
    public class ShapeException : Exception
    {
        public ShapeException(string message) : base(message)
        {
        }
    }
}