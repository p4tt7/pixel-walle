using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pixel_walle.src.Colores;
using System.Threading.Tasks;
using pixel_walle.src.Canvas;
using System.Drawing;
using pixel_walle.src.AST.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;

namespace pixel_walle.src.AST.Instructions.Functions
{
    public class Function_Library
    {

        public Dictionary<string, BuiltInFunction> BuiltIns = new()
        {
            ["Spawn"] = new BuiltInFunction
            {
                Parameters = new() { ExpressionType.Number, ExpressionType.Number },
                ReturnType = null,
                Implementation = (args, ctx) =>
                {
                    Spawn((int)args[0], (int)args[1], ctx);
                    return null;
                }
            },

            ["Color"] = new BuiltInFunction
            {
                Parameters = new() { ExpressionType.Text },
                ReturnType = null,
                Implementation = (args, ctx) =>
                {
                    Color((string)args[0], ctx);
                    return null;
                }

            },

            ["Size"] = new BuiltInFunction
            {
                Parameters = new() { ExpressionType.Number },
                ReturnType = null,
                Implementation = (args, ctx) =>
                {
                    Size((int)args[0], ctx);
                    return null;
                }

            },

            ["DrawLine"] = new BuiltInFunction
            {
                Parameters = new() { ExpressionType.Number, ExpressionType.Number, ExpressionType.Number },
                ReturnType = null,
                Implementation = (args, ctx) =>
                {
                    DrawLine((int)args[0], (int)args[1], (int)args[2], ctx);
                    return null;
                }
            },

            ["DrawRectangle"] = new BuiltInFunction
            {
                Parameters = new() { ExpressionType.Number, ExpressionType.Number, ExpressionType.Number },
                ReturnType = null,
                Implementation = (args, ctx) =>
                {
                    DrawRectangle((int)args[0], (int)args[1], (int)args[2], (int)args[3], ctx);
                    return null;
                }
            }

        };


        public static void Spawn(int x, int y, Context context)
        {
           context.Spawn(x, y);
        }

        public static void Color(string color, Context context)
        {
            foreach(var c in Colors.colores)
            {
                if(Colors.colores.ContainsKey(color))
                {
                    context.Brush.ColorBrush = Colors.colores[color];

                }

            }   

        }


        public static void Size(int k, Context context)
        {

            context.Brush.BrushThickness = k;

        }


        public static void DrawLine(int dirX, int dirY, int distance, Context context)
        {

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

        }


        public static void DrawCircle(int dirX, int dirY, int radius, Context context)
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

        }


        public static void  DrawRectangle(int dirX, int dirY, int distance, int width, int height, Context context)
        {

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

        }


        public static void Fill()
        {

        }


        public static int GetActualX(Context context)
        {
            return context.Robot.X;
            
        }

        public static int GetActualY(Context context)
        {
            return context.Robot.Y;
        }

        public static (int x, int y) GetCanvasSize(Context context)
        {
            return (context.canvas.Width, context.canvas.Height);
        }

        public static int GetColorCount(string color, int x1, int y1, int x2, int y2, Context context)
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

        public static int IsBrushSize(int size, Context context)
        {
            if(context.Brush.BrushThickness == size)
            {
                return 1;
            }

            return 0;
        }


        public static int IsCanvasColor(string color, int vertical, int horizontal, Context context)
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



        private static void Paint(Pixel pixel, Colors color)
        {
            pixel.Color = color;

        }



    }
}
