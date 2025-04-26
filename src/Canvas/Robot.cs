using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.Canvas
{
    public class Robot
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int BrushSize { get; set; }

        public Robot(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
}
