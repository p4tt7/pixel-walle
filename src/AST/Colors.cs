using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.Colores
{
    public class Colors
    {
        public int R {  get; private set; }
        public int G { get; private set; }
        public int B { get; private set; }

        public Colors(int r, int g, int b)
        {
            R = r; G = g; B = b;
        }

        public static Dictionary<string, Colors> colores = new Dictionary<string, Colors>
        {
            { "Red", new Colors(255, 0, 0) },
            { "Blue", new Colors(0, 0, 255) },
            { "Green", new Colors(0, 255, 0) },
            { "Yellow", new Colors(255, 255, 0) },
            { "Orange", new Colors(255, 165, 0) },
            { "Purple", new Colors(128, 0, 128) },
            { "Black", new Colors(0, 0, 0) },
            { "White", new Colors(255, 255, 255) },

        };
    }
}
