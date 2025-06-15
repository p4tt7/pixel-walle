using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions
{
    public class IsBrushSize : IFunction
    {
        public object? Execute(List<object?> arguments, Context context, List<Error> errors, CodeLocation location)
        {
            var size = (int)arguments[0];

            if (context.Brush.BrushThickness == size)
            {
                return 1;
            }

            return 0;
        }
    }
}
