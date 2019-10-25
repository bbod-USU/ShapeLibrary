using System.Drawing;
using System.IO;
using static Shapes.ImageManager;

namespace Shapes
{
    public class Image : Rectangle
    {
        public Image(Point point1, Point point2, Point point3, Point point4, Stream stream) : base(point1, point2,
            point3, point4)
        {
            Picture = GetImage(stream);
        }

        public Image(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4,
            Stream stream) : base(x1, y1, x2, y2, x3, y3, x4, y4)
        {
            Picture = GetImage(stream);
        }

        public Image(Point point, Size size, Stream stream) : base(point, size)
        {
            Picture = GetImage(stream);
        }

        public Bitmap Picture { get; }
    }
}