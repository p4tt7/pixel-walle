using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace pixel_walle.src.Canvas
{
    public class Brush
    {

        public Color ColorBrush { get; set; }
        public int BrushThickness { get; set; }

        public Brush(Color colorbrush, int brush_thickness)
        {
            ColorBrush = colorbrush;
            BrushThickness = 1;
        }
    }
}
