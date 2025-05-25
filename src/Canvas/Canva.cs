using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.Colores;

namespace pixel_walle.src.Canvas
{
    public class Canva
    {
        public Colors Color { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Dictionary<(int, int), Pixel> Pixeles { get; private set; }

        public Canva(int witdh, int height, Colors color)
        {
            Color = color;
            Width = witdh;
            Height = height;
            Pixeles = new Dictionary<(int, int), Pixel>();
        }
    }
}
