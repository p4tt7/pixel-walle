﻿using System;
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

            foreach (var (dx, dy) in directions)
            {
                for (int j = 1; j <= radius; j++)
                {
                    if (j == radius) 
                    {
                        DrawPixel(centerX + dx * j, centerY + dy * j, context);
                    }
                }
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
            if (x >= 0 && x < context.canvas.Width && y >= 0 && y < context.canvas.Height)
            {
                Pixel pixel = context.canvas.pixels[x, y];
                pixel.Color = context.Brush.ColorBrush; 
            }

        }

        private static void UpdateRobotPosition(int x, int y, Context context)
        {
            context.Robot.X = x;
            context.Robot.Y = y;
        }



    }
}
