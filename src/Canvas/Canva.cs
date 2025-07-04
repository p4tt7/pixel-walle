﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace pixel_walle.src.Canvas
{
    public class Canva
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Pixel[,] pixels;
        public bool[,] changes;

        public Canva(int width, int height)
        {
            Width = width;
            Height = height;

            pixels = new Pixel[Width, Height];
            changes = new bool[Width, Height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    pixels[x, y] = new Pixel(x, y, ColorPalette.Colors["White"]);
                    changes[x, y] = false;
                }
            }
        }




    }
}
