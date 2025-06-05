using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.Colores;

namespace pixel_walle.src.Canvas
{
    public class Pixel
    {
        public Colors Color { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public Pixel(int x, int y, Colors color) 
        {
            X = X;
            Y = y;
            Color = color;
        }

    }
}
