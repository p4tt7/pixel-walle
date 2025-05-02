using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.Colores;

namespace pixel_walle.src.Canvas
{
    public class Brush
    {

        public Color ColorBrush { get; private set; }
        public int BrushThickness { get; private set; }

        public Brush()
        {
            ColorBrush = Color.colores["White"];
            BrushThickness = 0;
        }
    }
}
