using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace InflateARectangle
{
    class Program
    {
        static void Inflate(ref Rectangle rect, Size inflateSize)
        {
            rect.X -= inflateSize.Width;
            rect.Y -= inflateSize.Height;
            rect.Width += 2 * inflateSize.Width;
            rect.Height += 2 * inflateSize.Height;
        }

        static void Main(string[] args)
        {
            Rectangle rect = new Rectangle(new Point(0, 0), new Size(10, 10));
            Inflate(ref rect, new Size(2, 2));
            Console.WriteLine(rect);
            Console.ReadKey();
        }
    }
}
