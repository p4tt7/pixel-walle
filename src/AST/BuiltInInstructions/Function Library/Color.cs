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
    public class Color_ : IInstructionFunction
    {

        public void Execute(List<object?> arguments, Context context, List<Error> errors, CodeLocation location)
        {
            var color = (string)arguments[0];

            if (ColorPalette.Colors.TryGetValue(color, out var brushColor))
            {
                context.Brush.ColorBrush = brushColor; 
            }
            else
            {
                errors.Add(new Error(
                    Error.ErrorType.Undefined,
                    $"Color '{color}' is not defined.",
                    location
                ));
            }
        }
    }
}
