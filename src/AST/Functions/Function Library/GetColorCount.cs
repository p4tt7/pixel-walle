using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace pixel_walle.src.AST.Expressions
{
    public class GetColorCount : IFunction
    {
        public List<ExpressionType> ParameterTypes => new List<ExpressionType> {ExpressionType.Text, ExpressionType.Number, ExpressionType.Number, ExpressionType.Number, ExpressionType.Number};

        public ExpressionType ReturnType => ExpressionType.Number;

        public object? Execute(List<object?> arguments, Context context, List<Error> errors, CodeLocation location)
        {
            var color = (string)arguments[0];
            int x1 = (int)arguments[1];
            var y1 = (int)arguments[2];
            var x2 = (int)arguments[3];
            var y2 = (int)arguments[4];

            var color_count = 0;

            if (x1 < 0 || x1 >= context.canvas.Width || y1 < 0 || y1 >= context.canvas.Height || x2 < 0 || x2 >= context.canvas.Width || y2 < 0 || y2 >= context.canvas.Height)
            {
                errors.Add(new Error(Error.ErrorType.OutOfRange, "Position parameter is out of range", location));
                return 0;
            }

            if(!ColorPalette.Colors.ContainsKey(color)) 
            {
                errors.Add(new Error(Error.ErrorType.Undefined, $"Color {color} is not defined", location));
                return null;

            }


            for (int i = x1; i <= x2; i++)
            {
                for (int j = y1; j <= y2; j++)
                {
                    if (context.canvas.pixels[i, j].Color.ToString() == color)
                    {
                        color_count++;
                    }

                }
            }

            return color_count;
        }
    }
}
