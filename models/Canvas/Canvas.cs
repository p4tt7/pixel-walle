using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;

namespace pixel_walle.models.Canvas
{
    class Canvas
    {
        public int Dimension { get; set; }
        public Color BackgroundColor { get; set; }
        public Pixel[,] Pixels {  get; private set; }
    }
}
