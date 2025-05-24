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

        public Colors ColorBrush { get; set; }
        public int BrushThickness { get; set; }

        public Brush()
        {
            ColorBrush = Colors.colores["White"];
            BrushThickness = 0;
        }
    }
}
