using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace pixel_walle.src.Canvas
{
    public class Pixel
    {
        public Color Color { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public Pixel(int x, int y, Color color) 
        {
            X = x;
            Y = y;
            Color = color;
        }

    }
}
