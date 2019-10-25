using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Shapes
{
    public class ShapeFactory
    {
        public T GetShapeFromFile<T>(FileStream fileStream)
        {
            FileIO fi = new FileIO();
            return fi.GetShapeFromFile<T>(fileStream);
        }
    }
}