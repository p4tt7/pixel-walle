using System;
using pixel_walle.src.CodeLocation_;
using pixel_walle.src.AST.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using pixel_walle.src.Errors;

namespace pixel_walle.src.AST.Instructions
{
    public interface IInstructionFunction
    {
        void Execute(List<object?> arguments, Context context, List<Error> errors, CodeLocation location);
    }

}
