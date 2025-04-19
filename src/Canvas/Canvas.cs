using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.Canvas
{
    public class Canvas
    {
        public string Color { get; set; }
        public Dictionary<(int, int), Pixel> Pixeles { get; private set; }

        public Canvas(string color)
        {
            Color = color;
            Pixeles = new Dictionary<(int, int), Pixel>();
        }
    }
}
