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
    public class Spawn : IInstructionFunction
    {

        public void Execute(List<object?> arguments, Context context, List<Error> errors, CodeLocation location)
        {
            var x = (int)arguments[0];
            var y = (int)arguments[1];

            if (x < 0 || x >= context.canvas.Width || y < 0 || y >= context.canvas.Height)
            {
                errors.Add(new Error(Error.ErrorType.OutOfRange,
                "Target position is outside the canvas.", location));
                return;
            }

            if (!context.HasRobot)
            {
                context.Spawn(x, y);
                context.canvas.changes[x,y] = true;
            }
            else
            {
                errors.Add(new Error(Error.ErrorType.AssignmentError, "Robot has already been spawned", location));
            }
        }
    }
}
