using pixel_walle.src.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Instructions
{
    public static class CanvasUtils
    {
        public static void DrawPixel(int x, int y, Context context)
        {
            int thickness = context.Brush.BrushThickness;

            if (thickness <= 0)
                return;

            if (context.Brush.ColorBrush == ColorPalette.Colors["Transparent"])
                return;

            int half = thickness / 2;

            for (int dx = -half; dx <= half; dx++)
            {
                for (int dy = -half; dy <= half; dy++)
                {
                    int px = x + dx;
                    int py = y + dy;

                    if (px >= 0 && px < context.canvas.Width && py >= 0 && py < context.canvas.Height)
                    {
                        context.canvas.pixels[px, py].Color = context.Brush.ColorBrush;
                        context.canvas.changes[px, py] = true;

                    }
                }
            }
        }



        public static void UpdateRobotPosition(int x, int y, Context context)
        {
            context.Robot.X = x;
            context.Robot.Y = y;
        }


        public static void ClearPixels(Context context, Color color)
        {
            for (int y = 0; y < context.canvas.Height; y++)
            {
                for (int x = 0; x < context.canvas.Width; x++)
                {
                    context.canvas.pixels[x, y] = new Pixel(x,y,color);
                }
            }
        }

        public static void MarkAllChanged(Context context)
        {
            for (int y = 0; y < context.canvas.Height; y++)
            {
                for (int x = 0; x < context.canvas.Width; x++)
                {
                    context.canvas.changes[x, y] = true;
                }
            }
        }

        public static void ClearCanvas(Context context, Color backgroundColor)
        {
            ClearPixels(context, backgroundColor);
            MarkAllChanged(context);
        }

    }
}
