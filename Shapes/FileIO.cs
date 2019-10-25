using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using static System.Runtime.Serialization.Json.JsonReaderWriterFactory;

namespace Shapes
{
    public class FileIO
    {
        public void SaveShape(Stream stream, Shape shape)
        {
            DataContractJsonSerializer a = new DataContractJsonSerializer(typeof(Shape));
            a.WriteObject(stream, shape);
//            DataContractJsonSerializer j = new DataContractJsonSerializer(typeof(Shape));
//            j.WriteObject(stream, shape);
        }

        public T GetShapeFromFile<T>(Stream stream)
        {
            var a = new DataContractJsonSerializer(typeof(T));
            return  (T) a.ReadObject(stream);
        }
        
    }
}