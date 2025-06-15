using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions
{
    public class IsCanvasColor : IFunction
    {

        public object? Execute(List<object?> arguments, Context context, List<Error> errors, CodeLocation location)
        {
            var color = (string)arguments[0];
            var horizontal = (int)arguments[1];
            var vertical = (int)arguments[2];

            if(vertical < context.canvas.Height || vertical > context.canvas.Height || horizontal < context.canvas.Width || horizontal > context.canvas.Width)
            {
                errors.Add(new Error(Error.ErrorType.OutOfRange, "Position parameter is out of range", location));
                return 0;
            }

            if (!ColorPalette.Colors.ContainsKey(color))
            {
                errors.Add(new Error(Error.ErrorType.Undefined, $"Color {color} is not defined", location));
                return null;

            }

            int x = context.Robot.X;
            int y = context.Robot.Y;

            int posx = x + horizontal;
            int posy = y + vertical;

            if (context.canvas.pixels[posx, posy].Color.ToString() == color)
            {
                return 1;
            }

            return 0;
        }
    }
}
