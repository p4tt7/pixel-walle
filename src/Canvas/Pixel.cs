using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.Canvas
{
    public class Pixel
    {
        public (int X, int Y) Posicion { get; set; }
        public string Color { get; set; }

        public Pixel(int x, int y, string color)
        {
            Posicion = (x, y);
            Color = color;
        }
    }
}
