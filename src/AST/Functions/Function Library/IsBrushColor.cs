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

            var colorName = (string)arguments[0];

            if (!ColorPalette.Colors.TryGetValue(colorName, out var expectedColor))
            {
                errors.Add(new Error(
                Error.ErrorType.SemanticError,
                $"Color '{colorName}' is not defined.",
                location
                ));
                return null;
            }

            var currentColor = context.Brush.ColorBrush;

            if (currentColor.Equals(expectedColor))
            {
                return 1;
            }

            return 0;

        }
    }
}
