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
            if (context.Robot == null)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, "Robot context is null.", location));
                return;
            }

            if (arguments.Count < 5)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, "Missing arguments for rectangle drawing.", location));
                return;
            }

            var dirX = (int)arguments[0];
            var dirY = (int)arguments[1];
            var distance = (int)arguments[2];
            var width = (int)arguments[3];
            var height = (int)arguments[4];

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

            if (width <= 0 || height <= 0)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, "Width and height must be positive integers.", location));
                return;
            }

            int centerX = context.Robot.X + dirX * distance;
            int centerY = context.Robot.Y + dirY * distance;

            int startX = centerX - (width - 1) / 2;  
            int startY = centerY - (height - 1) / 2;
            int endX = startX + width - 1;
            int endY = startY + height - 1;

            if (startX < 0 || startY < 0 || endX >= context.canvas.Width || endY >= context.canvas.Height)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, "Rectangle would be drawn outside the canvas boundaries.", location));
                return;
            }

            for (int x = startX; x <= endX; x++)
            {
                CanvasUtils.DrawPixel(x, startY, context); 
                CanvasUtils.DrawPixel(x, endY, context); 
            }

            for (int y = startY + 1; y < endY; y++)
            {
                CanvasUtils.DrawPixel(startX, y, context); 
                CanvasUtils.DrawPixel(endX, y, context);   
            }

            context.Robot.X = centerX;
            context.Robot.Y = centerY;
        }
    }
}