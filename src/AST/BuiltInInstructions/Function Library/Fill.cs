using pixel_walle.src.AST.Expressions;
using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using pixel_walle.src.Canvas;
using System;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Instructions
{
    public class Fill : IInstructionFunction
    {

            public void Execute(List<object?> arguments, Context context, List<Error> errors, CodeLocation location)
            {
                if (context.Robot == null)
                {
                    errors.Add(new Error(
                        Error.ErrorType.Undefined,
                        "Cannot perform fill: Robot is not defined in the current context.",
                        location));
                    return;
                }

                int startX = context.Robot.X;
                int startY = context.Robot.Y;

                if (startX < 0 || startX >= context.canvas.Width || startY < 0 || startY >= context.canvas.Height)
                {
                    errors.Add(new Error(
                        Error.ErrorType.OutOfRange,
                        "The robot is outside the canvas bounds.",
                        location));
                    return;
                }

                Color targetColor = context.canvas.pixels[startX, startY].Color;
                Color fillColor = context.Brush.ColorBrush;

                if (ColorsSimilar(targetColor, fillColor))
                    return;

                FloodFill(context, startX, startY, targetColor, fillColor);
            }

            private static void FloodFill(Context context, int x, int y, Color targetColor, Color fillColor)
            {
                int width = context.canvas.Width;
                int height = context.canvas.Height;
                bool[,] visited = new bool[width, height];

                var toProcess = new List<(int x, int y)>();
                toProcess.Add((x, y));
                int index = 0;

                while (index < toProcess.Count)
                {
                    var (cx, cy) = toProcess[index++];
                    if (cx < 0 || cx >= width || cy < 0 || cy >= height)
                        continue;

                    if (visited[cx, cy])
                        continue;

                    Color current = context.canvas.pixels[cx, cy].Color;
                    if (!ColorsSimilar(current, targetColor))
                        continue;

                    context.canvas.pixels[cx, cy].Color = fillColor;
                    context.canvas.changes[cx, cy] = true;
                    visited[cx, cy] = true;

                    toProcess.Add((cx + 1, cy));
                    toProcess.Add((cx - 1, cy));
                    toProcess.Add((cx, cy + 1));
                    toProcess.Add((cx, cy - 1));
                }
            }

            private static bool ColorsSimilar(Color a, Color b, int threshold = 30)
            {
                return Math.Abs(a.R - b.R) < threshold &&
                       Math.Abs(a.G - b.G) < threshold &&
                       Math.Abs(a.B - b.B) < threshold;
            }


    }
}
