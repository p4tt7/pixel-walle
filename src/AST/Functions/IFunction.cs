using System;
using pixel_walle.src.CodeLocation_;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using pixel_walle.src.Errors;

namespace pixel_walle.src.AST.Expressions
{
    public interface IFunction
    {
        object? Execute(List<object?> arguments, Context context, List<Error> errors, CodeLocation location);
    }


}
