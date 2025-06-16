using pixel_walle.src.AST.Instructions;
using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.BuiltInInstructions.Function_Library
{
    public class DrawRhombus : IInstructionFunction
    {
        public void Execute(List<object?> arguments, Context context, List<Error> errors, CodeLocation location)
        {
            if (context.Robot == null)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, "Robot context is null.", location));
                return;
            }

            var dirX = (int)arguments[0];
            var dirY = (int)arguments[1];
            var distance = (int)arguments[2];
            var size = (int)arguments[3];

            if (!(dirX == -1 || dirX == 0 || dirX == 1) || !(dirY == -1 || dirY == 0 || dirY == 1))
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, "Direction components must be -1, 0, or 1.", location));
                return;
            }

            if (distance < 0)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, "Distance must be non-negative.", location));
                return;
            }

            if (size <= 0)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, "Size must be a positive integer.", location));
                return;
            }

            int centerX = context.Robot.X + dirX * distance;
            int centerY = context.Robot.Y + dirY * distance;

            int canvasWidth = context.canvas.Width;
            int canvasHeight = context.canvas.Height;

            if (centerX - size < 0 || centerX + size >= canvasWidth ||
                centerY - size < 0 || centerY + size >= canvasHeight)
            {
                errors.Add(new Error(Error.ErrorType.OutOfRange, $"Center ({centerX}, {centerY}) is outside the canvas.", location));
                return;
            }

            context.Robot.X = centerX;
            context.Robot.Y = centerY;

            for (int y = centerY - size; y <= centerY + size; y++)
            {
                int dy = Math.Abs(y - centerY);
                int xLeft = centerX - (size - dy);
                int xRight = centerX + (size - dy);

                CanvasUtils.DrawPixel(xLeft, y, context);
                CanvasUtils.DrawPixel(xRight, y, context);
            }
        }

    }
}
