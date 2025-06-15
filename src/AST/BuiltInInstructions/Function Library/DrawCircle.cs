using pixel_walle.src.AST.Expressions;
using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Instructions
{
    public class DrawCircle : IInstructionFunction
    {

        public void Execute(List<object?> arguments, Context context, List<Error> errors, CodeLocation location)
        {
            var dirX = (int)arguments[0];
            var dirY = (int)arguments[1];
            var radius = (int)arguments[2];

            if (Math.Abs(dirX) > 1 || Math.Abs(dirY) > 1)
            {
                errors.Add(new Error(
                    Error.ErrorType.SemanticError,
                    $"Invalid direction ({dirX}, {dirY}). Directions must be -1, 0, or 1.",
                    location));
                return;
            }

            if (context.Robot == null)
            {
                errors.Add(new Error(Error.ErrorType.Undefined,
                    "Cannot draw circle: Robot is not defined in the current context.",
                    location));
                return;
            }

            if (radius <= 0)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError,
                    "Radius must be a positive integer.", location));
                return;
            }

            int centerX = context.Robot.X + dirX * radius;
            int centerY = context.Robot.Y + dirY * radius;

            if (centerX < 0 || centerX >= context.canvas.Width ||
                centerY < 0 || centerY >= context.canvas.Height)
            {
                errors.Add(new Error(Error.ErrorType.OutOfRange,
                    $"Circle center ({centerX}, {centerY}) is outside the canvas.",
                    location));
                return;
            }

            for (int angle = 0; angle < 360; angle += 1)
            {
                double rad = angle * Math.PI / 180.0;
                int x = centerX + (int)Math.Round(radius * Math.Cos(rad));
                int y = centerY + (int)Math.Round(radius * Math.Sin(rad));

                if (x >= 0 && x < context.canvas.Width && y >= 0 && y < context.canvas.Height)
                {
                    CanvasUtils.DrawPixel(x, y, context);
                }
            }

            CanvasUtils.UpdateRobotPosition(centerX, centerY, context);
        }
    }
}