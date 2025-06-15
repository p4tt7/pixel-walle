using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions
{
    public class GetActualX : IFunction
    {

        public object? Execute(List<object?> arguments, Context context, List<Error> errors, CodeLocation location)
        {
            if(context.HasRobot)
            {
                return context.Robot.X;
            }

            errors.Add(new Error(Error.ErrorType.Undefined, "There's no robot defined in the current context", location));
            return -1;
        }
    }

}
