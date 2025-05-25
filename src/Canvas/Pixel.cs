using System;
using System.Collections.Generic;
using System.Linq;
using pixel_walle.src.Colores;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.Canvas
{
    public class Pixel
    {

        public Colors Color { get; set; }

        public Pixel(Colors color)
        {
            Color = color;
        }

    }

}
