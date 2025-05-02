using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.Colores
{
    public class Color
    {
        public int R {  get; private set; }
        public int G { get; private set; }
        public int B { get; private set; }

        public Color(int r, int g, int b)
        {
            R = r; G = g; B = b;
        }

        public static Dictionary<string, Color> colores = new Dictionary<string, Color>
        {
            { "Red", new Color(255, 0, 0) },
            { "Blue", new Color(0, 0, 255) },
            { "Green", new Color(0, 255, 0) },
            { "Yellow", new Color(255, 255, 0) },
            { "Orange", new Color(255, 165, 0) },
            { "Purple", new Color(128, 0, 128) },
            { "Black", new Color(0, 0, 0) },
            { "White", new Color(255, 255, 255) },

        };
    }
}
