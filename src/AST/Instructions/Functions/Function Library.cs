using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pixel_walle.src.Colores;
using System.Threading.Tasks;
using pixel_walle.src.Canvas;
using System.Drawing;
using pixel_walle.src.AST.Expressions;

namespace pixel_walle.src.AST.Instructions.Functions
{
    public class Function_Library
    {
        public void Spawn(int x, int y, Context context)
        {
           context.Spawn(x, y);
        }

        public bool Color(string color, Context context)
        {
            foreach(var c in Colors.colores)
            {
                if(Colors.colores.ContainsKey(color))
                {
                    context.Brush.ColorBrush = Colors.colores[color];
                    return true;

                }

            }   
            
            return false;
        }


        public void Size(int k, Context context)
        {

            context.Brush.BrushThickness = k;

        }


        public bool DrawLine(int dirX, int dirY, int distance, Context context)
        {
            if (context.Robot == null)
                return false;

            int x = context.Robot.X;
            int y = context.Robot.Y;

            for (int step = 0; step <= distance; step++)
            {
                int currentX = x + step * dirX;
                int currentY = y + step * dirY;

                var pos = (currentX, currentY);

                if (context.canvas.Pixeles.TryGetValue(pos, out var pixel))
                {
                    Paint(pixel, context.Brush.ColorBrush);
                }

                else
                {
                    var newPixel = new Pixel(context.Brush.ColorBrush);
                    context.canvas.Pixeles[pos] = newPixel;
                }

                context.Robot.X = currentX;
                context.Robot.Y = currentY;

            }

            return true;
        }


        public bool DrawCircle(int dirX, int dirY, int radius, Context context)
        {
            (int dx, int dy)[] directions = new (int, int)[]
            {
            ( 0, -1),  
            ( 1, -1), 
            ( 1,  0),  
            ( 1,  1),  
            ( 0,  1),  
            (-1,  1),  
            (-1,  0),  
            (-1, -1)   
            };

            if (context.Robot == null)
                return false;

            int startX = context.Robot.X;
            int startY = context.Robot.Y;

            foreach (var (dx, dy) in directions)
            {
                for (int j = 1; j <= radius; j++)
                {
                    int currentX = startX + dx * j;
                    int currentY = startY + dy * j;

                    if (j == radius)
                    {
                        var pos = (currentX, currentY);

                        if (context.canvas.Pixeles.TryGetValue(pos, out var pixel))
                        {
                            Paint(pixel, context.Brush.ColorBrush);
                        }
                        else
                        {
                            var newPixel = new Pixel(context.Brush.ColorBrush);
                            context.canvas.Pixeles[pos] = newPixel;
                        }
                    }
                }
            }

            context.Robot.X += dirX * radius;
            context.Robot.Y += dirY * radius;

            return true;
        }


        public bool DrawRectangle(int dirX, int dirY, int distance, int width, int height, Context context)
        {
            if (context.Robot == null)
                return false;

            context.Robot.X += dirX * distance;
            context.Robot.Y += dirY * distance;

            int centerX = context.Robot.X;
            int centerY = context.Robot.Y;

            int halfWidth = width / 2;
            int halfHeight = height / 2;

            for (int dx = -halfWidth; dx <= halfWidth; dx++)
            {
                for (int dy = -halfHeight; dy <= halfHeight; dy++)
                {
                    int x = centerX + dx;
                    int y = centerY + dy;
                    var pos = (x, y);

                    if (context.canvas.Pixeles.TryGetValue(pos, out var pixel))
                    {
                        Paint(pixel, context.Brush.ColorBrush);
                    }
                    else
                    {
                        var newPixel = new Pixel(context.Brush.ColorBrush);

                        context.canvas.Pixeles[pos] = newPixel;
                    }
                }
            }

            return true;
        }


        public void Fill()
        {

        }


        public int GetActualX(Context context)
        {
            return context.Robot.X;
            
        }

        public int GetActualY(Context context)
        {
            return context.Robot.Y;
        }

        public (int x, int y) GetCanvasSize(Context context)
        {
            return (context.canvas.Width, context.canvas.Height);
        }

        public int GetColorCount(string color, int x1, int y1, int x2, int y2, Context context)
        {

            if (!Colors.colores.TryGetValue(color, out Colors targetColor))
            {
                return 0;
            }

            int color_count = 0;

            for(int i = x1; i<=x2; i++)
            {
                for(int j= y1; j<=y2; j++)
                {
                    if(context.canvas.Pixeles.TryGetValue((i,j), out Pixel pixel) && pixel.Color==targetColor)
                    {
                        color_count++;
                    }

                }
            }

            return color_count;
           
        }

        public int IsBrushSize(int size, Context context)
        {
            if(context.Brush.BrushThickness == size)
            {
                return 1;
            }

            return 0;
        }


        public int IsCanvasColor(string color, int vertical, int horizontal, Context context)
        {
            int x = context.Robot.X;
            int y = context.Robot.Y;

            int posx = x + horizontal;
            int posy = y + vertical;

            if (!Colors.colores.TryGetValue(color, out Colors targetColor))
            {
                return 0;
            }

            if (context.canvas.Pixeles.TryGetValue((posx, posy), out Pixel pixel) &&
                pixel.Color == targetColor)
            {
                return 1;
            }

            return 0;
        }



        private void Paint(Pixel pixel, Colors color)
        {
            pixel.Color = color;

        }



    }
}
