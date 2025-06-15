using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace pixel_walle.src
{
    public static class ColorPalette
    {
        public static readonly Dictionary<string, Color> Colors = new()
    {
        { "Red", Color.FromRgb(255, 0, 0) },
        { "Blue", Color.FromRgb(0, 0, 255) },
        { "Green", Color.FromRgb(0, 255, 0) },
        { "Yellow", Color.FromRgb(255, 255, 0) },
        { "Orange", Color.FromRgb(255, 165, 0) },
        { "Purple", Color.FromRgb(128, 0, 128) },
            {"Pink", Color.FromRgb(255, 192, 203) },
        { "Black", Color.FromRgb(0, 0, 0) },
        { "White", Color.FromRgb(255, 255, 255) },
        { "Transparent", Color.FromArgb(0, 0, 0, 0) }
    };
    }


}
