using System.IO;

namespace Shapes
{
    public interface IFileIO
    {
        void SaveShape(Stream stream, Shape shape);
        T GetShapeFromFile<T>(Stream stream);
    }
}