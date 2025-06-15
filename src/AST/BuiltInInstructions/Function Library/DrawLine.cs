using pixel_walle.src.AST.Expressions;
using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Instructions
{
    public class DrawLine : IInstructionFunction
    {
        public void Execute(List<object?> arguments, Context context, List<Error> errors, CodeLocation location)
        {
            var dirX = (int)arguments[0];
            var dirY = (int)arguments[1];
            var distance = (int)arguments[2];

            if (Math.Abs(dirX) > 1 || Math.Abs(dirY) > 1)
            {
                errors.Add(new Error(
                    Error.ErrorType.SemanticError,
                    $"Invalid direction ({dirX}, {dirY}). Directions must be -1, 0, or 1.",
                    location));
                return;
            }


            int startX = context.Robot.X;
            int startY = context.Robot.Y;

            int canvasWidth = context.canvas.Width;
            int canvasHeight = context.canvas.Height;

            int lastValidX = startX;
            int lastValidY = startY;

            for (int step = 0; step <= distance; step++)
            {
                int currentX = startX + step * dirX;
                int currentY = startY + step * dirY;

                if (currentX >= 0 && currentX < canvasWidth &&
                    currentY >= 0 && currentY < canvasHeight)
                {
                    CanvasUtils.DrawPixel(currentX, currentY, context);
                    lastValidX = currentX;
                    lastValidY = currentY;
                }
                else
                {
                    errors.Add(new Error(
                        Error.ErrorType.OutOfRange,
                        "Part of the line is outside the canvas.",
                        location));
                    break; 
                }
            }

            CanvasUtils.UpdateRobotPosition(lastValidX, lastValidY, context);
        }
    }
}