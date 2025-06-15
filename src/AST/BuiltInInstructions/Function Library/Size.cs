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

            if(k<=0)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, "Brush thickess cannot be 0 or a negative number", location));
            }

            context.Brush.BrushThickness = k;
        }
    }
}
