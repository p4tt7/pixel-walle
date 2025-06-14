using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.Canvas;
using System.Windows.Media;
using pixel_walle.src.AST.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;

namespace pixel_walle.src.AST.Instructions.Functions
{
    public class FunctionLibrary
    {

        public static Dictionary<string, FunctionInfo> BuiltIns = new()
        {
            ["Spawn"] = new FunctionInfo
            {
                Parameters = new() { ExpressionType.Number, ExpressionType.Number },
                ReturnType = null,
                Implementation = (args, ctx) =>
                {
                    Spawn((int)args[0], (int)args[1], ctx);
                    return null;
                }
            },

            ["Color"] = new FunctionInfo
            {
                Parameters = new() { ExpressionType.Text },
                ReturnType = null,
                Implementation = (args, ctx) =>
                {
                    Color((string)args[0], ctx);
                    return null;
                }
            },

            ["Size"] = new FunctionInfo
            {
                Parameters = new() { ExpressionType.Number },
                ReturnType = null,
                Implementation = (args, ctx) =>
                {
                    Size((int)args[0], ctx);
                    return null;
                }
            },

            ["DrawLine"] = new FunctionInfo
            {
                Parameters = new() { ExpressionType.Number, ExpressionType.Number, ExpressionType.Number },
                ReturnType = null,
                Implementation = (args, ctx) =>
                {
                    DrawLine((int)args[0], (int)args[1], (int)args[2], ctx);
                    return null;
                }
            },

            ["DrawRectangle"] = new FunctionInfo
            {
                Parameters = new() { ExpressionType.Number, ExpressionType.Number, ExpressionType.Number, ExpressionType.Number, ExpressionType.Number },
                ReturnType = null,
                Implementation = (args, ctx) =>
                {
                    DrawRectangle((int)args[0], (int)args[1], (int)args[2], (int)args[3], (int)args[4], ctx);
                    return null;
                }
            },

            ["DrawCircle"] = new FunctionInfo
            {
                Parameters = new() { ExpressionType.Number, ExpressionType.Number, ExpressionType.Number },
                ReturnType = null,
                Implementation = (args, ctx) =>
                {
                    DrawCircle((int)args[0], (int)args[1], (int)args[2], ctx);
                    return null;
                }
            },

            ["GetActualX"] = new FunctionInfo
            {
                Parameters = new(),
                ReturnType = ExpressionType.Number,
                Implementation = (args, ctx) =>
                {
                    return GetActualX(ctx);
                }
            },

            ["GetActualY"] = new FunctionInfo
            {
                Parameters = new(),
                ReturnType = ExpressionType.Number,
                Implementation = (args, ctx) =>
                {
                    return GetActualY(ctx);
                }
            },

            ["GetCanvasSize"] = new FunctionInfo
            {
                Parameters = new(),
                ReturnType = ExpressionType.Number, 
                Implementation = (args, ctx) =>
                {
                    var (x, y) = GetCanvasSize(ctx);
                    return new Tuple<int, int>(x, y);
                }
            },

            ["GetColorCount"] = new FunctionInfo
            {
                Parameters = new() { ExpressionType.Text, ExpressionType.Number, ExpressionType.Number, ExpressionType.Number, ExpressionType.Number },
                ReturnType = ExpressionType.Number,
                Implementation = (args, ctx) =>
                {
                    return GetColorCount((string)args[0], (int)args[1], (int)args[2], (int)args[3], (int)args[4], ctx);
                }
            },

            ["IsBrushSize"] = new FunctionInfo
            {
                Parameters = new() { ExpressionType.Number },
                ReturnType = ExpressionType.Number,
                Implementation = (args, ctx) =>
                {
                    return IsBrushSize((int)args[0], ctx);
                }
            },

            ["IsCanvasColor"] = new FunctionInfo
            {
                Parameters = new() { ExpressionType.Text, ExpressionType.Number, ExpressionType.Number },
                ReturnType = ExpressionType.Number,
                Implementation = (args, ctx) =>
                {
                    return IsCanvasColor((string)args[0], (int)args[1], (int)args[2], ctx);
                }
            }

        };



        public static void Spawn(int x, int y, Context context)
        {
            if(!context.HasRobot)
           context.Spawn(x, y);
        }



        public static void Color(string color, Context context)
        {
            foreach(var c in ColorPalette.Colors)
            {
                if(ColorPalette.Colors.ContainsKey(color))
                {
                    context.Brush.ColorBrush = ColorPalette.Colors[color];

                }

            }   

        }


        public static void Size(int k, Context context)
        {

            context.Brush.BrushThickness = k;

        }





        public static void Fill(Context context)
        {
            int startX = context.Robot.X;
            int startY = context.Robot.Y;

            if (startX < 0 || startX >= context.canvas.Width ||
                startY < 0 || startY >= context.canvas.Height)
            {
                return;
            }

            Color targetColor = context.canvas.pixels[startX, startY].Color;

            Color fillColor = context.Brush.ColorBrush;

            if (targetColor.Equals(fillColor))
            {
                return;
            }

            FloodFillRecursive(context, startX, startY, targetColor, fillColor);
        }

        private static void FloodFillRecursive(Context context, int x, int y, Color targetColor, Color fillColor)
        {
            if (x < 0 || x >= context.canvas.Width || y < 0 || y >= context.canvas.Height)
            {
                return;
            }

            Pixel currentPixel = context.canvas.pixels[x, y];

            if (!currentPixel.Color.Equals(targetColor) || currentPixel.Color.Equals(fillColor))
            {
                return;
            }

            DrawPixel(x, y, context);

            FloodFillRecursive(context, x + 1, y, targetColor, fillColor); // derecha
            FloodFillRecursive(context, x - 1, y, targetColor, fillColor); // izquierda
            FloodFillRecursive(context, x, y + 1, targetColor, fillColor); // abajo
            FloodFillRecursive(context, x, y - 1, targetColor, fillColor); // arriba
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

            int color_count = 0;

            for(int i = x1; i<=x2; i++)
            {
                for(int j= y1; j<=y2; j++)
                {
                    if (context.canvas.pixels[i,j].Color.ToString() == color)
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

            if (context.canvas.pixels[posx,posy].Color.ToString() == color)
            {
                return 1;
            }

            return 0;
        }

        public static void DrawLine(int dirX, int dirY, int distance, Context context)
        {
            int startX = context.Robot.X;
            int startY = context.Robot.Y;

            for (int step = 0; step <= distance; step++)
            {
                int currentX = startX + step * dirX;
                int currentY = startY + step * dirY;
                DrawPixel(currentX, currentY, context); 
            }

            UpdateRobotPosition(startX + distance * dirX, startY + distance * dirY, context); 
        }

        public static void DrawCircle(int dirX, int dirY, int radius, Context context)
        {
            (int dx, int dy)[] directions = new (int, int)[]
            {
        (0, -1), (1, -1), (1, 0), (1, 1),
        (0, 1), (-1, 1), (-1, 0), (-1, -1)
            };

            int centerX = context.Robot.X;
            int centerY = context.Robot.Y;

            for (int angle = 0; angle < 360; angle += 10) 
            {
                double rad = angle * Math.PI / 180.0;
                int x = centerX + (int)Math.Round(radius * Math.Cos(rad));
                int y = centerY + (int)Math.Round(radius * Math.Sin(rad));
                DrawPixel(x, y, context);
            }

            UpdateRobotPosition(centerX + dirX * radius, centerY + dirY * radius, context);
        }


        public static void DrawRectangle(int dirX, int dirY, int distance, int width, int height, Context context)
        {
            int startX = context.Robot.X + dirX * distance;
            int startY = context.Robot.Y + dirY * distance;
            UpdateRobotPosition(startX, startY, context);

            int halfWidth = width / 2;
            int halfHeight = height / 2;

            for (int dx = -halfWidth; dx <= halfWidth; dx++)
            {
                for (int dy = -halfHeight; dy <= halfHeight; dy++)
                {
                    DrawPixel(startX + dx, startY + dy, context);
                }
            }
        }



        private static void DrawPixel(int x, int y, Context context)
        {
            int thickness = context.Brush.BrushThickness;
            int half = thickness / 2;

            for (int dx = -half; dx <= half; dx++)
            {
                for (int dy = -half; dy <= half; dy++)
                {
                    int px = x + dx;
                    int py = y + dy;

                    if (px >= 0 && px < context.canvas.Width && py >= 0 && py < context.canvas.Height)
                    {
                        Pixel pixel = context.canvas.pixels[px, py];
                        pixel.Color = context.Brush.ColorBrush;
                    }
                }
            }
        }


        private static void UpdateRobotPosition(int x, int y, Context context)
        {
            context.Robot.X = x;
            context.Robot.Y = y;
        }



    }
}
