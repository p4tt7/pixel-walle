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
                    location
                ));
                return;
            }


            int startX = context.Robot.X;
            int startY = context.Robot.Y;

            if (startX < 0 || startX >= context.canvas.Width || startY < 0 || startY >= context.canvas.Height)
            {
                errors.Add(new Error(
                    Error.ErrorType.OutOfRange,
                    "The robot is outside the canvas bounds.",
                    location
                ));
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

            CanvasUtils.DrawPixel(x, y, context);

            FloodFillRecursive(context, x + 1, y, targetColor, fillColor); // derecha
            FloodFillRecursive(context, x - 1, y, targetColor, fillColor); // izquierda
            FloodFillRecursive(context, x, y + 1, targetColor, fillColor); // abajo
            FloodFillRecursive(context, x, y - 1, targetColor, fillColor); // arriba
        }


    }
}
