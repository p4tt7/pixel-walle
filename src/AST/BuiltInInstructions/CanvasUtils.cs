using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    }
                }
            }
        }



        public static void UpdateRobotPosition(int x, int y, Context context)
        {
            context.Robot.X = x;
            context.Robot.Y = y;
        }

    }
}
