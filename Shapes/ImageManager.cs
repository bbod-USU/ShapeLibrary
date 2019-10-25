using System.Collections;
using System.Drawing;
using System.IO;

namespace Shapes
{
    public static class ImageManager
    {
        private static Hashtable _images;
        static ImageManager()
        {
            _images = new Hashtable();
        }

        internal static Bitmap GetImage(Stream stream)
        {
            if (_images.ContainsKey(stream))
            {
                return (Bitmap) _images[stream];
            }
            else
            {
               Bitmap bitmap = new Bitmap(stream);
               _images.Add(stream, bitmap);
               return bitmap;

            }
        }
        
        
    }
    
}