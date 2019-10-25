using System.IO;

namespace Shapes
{
    public class ShapeFactory
    {
        public T GetShapeFromFile<T>(FileStream fileStream)
        {
            var fi = new FileIO();
            return fi.GetShapeFromFile<T>(fileStream);
        }
    }
}