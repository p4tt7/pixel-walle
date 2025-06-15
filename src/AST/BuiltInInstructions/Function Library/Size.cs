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
    public class Size : IInstructionFunction
    {

        public void Execute(List<object?> arguments, Context context, List<Error> errors, CodeLocation location)
        {
            var k = (int)arguments[0];

            if (k <= 0)
            {
                errors.Add(new Error(
                    Error.ErrorType.SemanticError,
                    "Brush thickness must be a positive odd number (received " + k + ").",
                    location
                ));
                return;
            }

            int adjustedThickness = k;
            if (k % 2 == 0)
            {
                adjustedThickness = k - 1; 
               
            }

            context.Brush.BrushThickness = adjustedThickness; 
        }
    }
}
