using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions
{
    public class IsBrushColor : IFunction
    {

        public object? Execute(List<object?> arguments, Context context, List<Error> errors, CodeLocation location)
        {

            var color = (string)arguments[0];

            if (context.Brush.ColorBrush.ToString() == color)
            {
                return 1;
            }

            return 0;

        }
    }
}
