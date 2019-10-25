using System.IO;
using System.Runtime.Serialization.Json;

namespace Shapes
{
    public class FileIO : IFileIO
    {
        public void SaveShape(Stream stream, Shape shape)
        {
            var a = new DataContractJsonSerializer(typeof(Shape));
            a.WriteObject(stream, shape);
//            DataContractJsonSerializer j = new DataContractJsonSerializer(typeof(Shape));
//            j.WriteObject(stream, shape);
        }

        public T GetShapeFromFile<T>(Stream stream)
        {
            var a = new DataContractJsonSerializer(typeof(T));
            return (T) a.ReadObject(stream);
        }
    }
}