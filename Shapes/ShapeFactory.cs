using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Shapes
{
    [ExcludeFromCodeCoverage]
    public class ShapeFactory
    {
        public T GetShapeFromFile<T>(FileStream fileStream)
        {
            var fi = new FileIO();
            return fi.GetShapeFromFile<T>(fileStream);
        }
    }
}