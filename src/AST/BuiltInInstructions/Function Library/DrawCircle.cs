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


            (int dx, int dy)[] directions = new (int, int)[]
            {
                (0, -1), (1, -1), (1, 0), (1, 1),
                (0, 1), (-1, 1), (-1, 0), (-1, -1)
            };

            int centerX = context.Robot.X;
            int centerY = context.Robot.Y;

            int newX = centerX + dirX * radius;
            int newY = centerY + dirY * radius;

            if (newX < 0 || newX >= context.canvas.Width || newY < 0 || newY >= context.canvas.Height)
            {
                errors.Add(new Error(Error.ErrorType.OutOfRange,
                "The new robot position after movement is outside the canvas.",
                location));
                return;
            }

            for (int angle = 0; angle < 360; angle += 1)
            {
                double rad = angle * Math.PI / 180.0;
                int x = centerX + (int)Math.Round(radius * Math.Cos(rad));
                int y = centerY + (int)Math.Round(radius * Math.Sin(rad));
                CanvasUtils.DrawPixel(x, y, context);
            }

            CanvasUtils.UpdateRobotPosition(centerX + dirX, centerY + dirY, context);
        }
    }
}
