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
    public class DrawLine : IInstructionFunction
    {

        public void Execute(List<object?> arguments, Context context, List<Error> errors, CodeLocation location)
        {
            var dirX = (int)arguments[0];
            var dirY = (int)arguments[1];
            var distance = (int)arguments[2];


            int startX = context.Robot.X;
            int startY = context.Robot.Y;

            for (int step = 0; step <= distance; step++)
            {
                int currentX = startX + step * dirX;
                int currentY = startY + step * dirY;
                CanvasUtils.DrawPixel(currentX, currentY, context);
            }

            CanvasUtils.UpdateRobotPosition(startX + distance * dirX, startY + distance * dirY, context);
        }
    }
}
