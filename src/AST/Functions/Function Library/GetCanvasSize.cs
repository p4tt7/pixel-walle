using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions
{
    public class GetCanvasSize : IFunction
    {
        public object? Execute(List<object?> arguments, Context context, List<Error> errors, CodeLocation location)
        {
            int width = context.canvas.Width;
            int height = context.canvas.Height;

            if (width == height)
                return width.ToString();
            else
                return $"{width}x{height}"; 
        }
    }

}
