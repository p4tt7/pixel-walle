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
    public class DrawRectangle : IInstructionFunction
    {

        public void Execute(List<object?> arguments, Context context, List<Error> errors, CodeLocation location)
        {
            var dirX = (int)arguments[0];
            var dirY = (int)arguments[1];
            var distance = (int)arguments[2];
            var width = (int)arguments[3];
            var height = (int)arguments[4];

            if (distance <= 0)
            {
                  errors.Add(new Error(Error.ErrorType.SemanticError,
                  "Distance must be a positive integer.", location));
                  return;
            }

            if (width <= 0 || height <= 0)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError,
                "Width and height must be positive integers.", location));
                return;
            }



            int startX = context.Robot.X + dirX * distance;
            int startY = context.Robot.Y + dirY * distance;

            if (startX < 0 || startX >= context.canvas.Width || startY < 0 || startY >= context.canvas.Height)
            {
                errors.Add(new Error(Error.ErrorType.OutOfRange,
                "Target position is outside the canvas.", location));
                return;
            }


            CanvasUtils.UpdateRobotPosition(startX, startY, context);


            int halfWidth = width / 2;
            int halfHeight = height / 2;

            for (int dx = -halfWidth; dx <= halfWidth; dx++)
            {
                for (int dy = -halfHeight; dy <= halfHeight; dy++)
                {
                    CanvasUtils.DrawPixel(startX + dx, startY + dy, context);
                }
            }

        }
    }
}
