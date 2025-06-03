using pixel_walle.src.AST.Expressions;
using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Instructions.Functions
{
    public class FunctionInfo
    {

        public string FunctionName { get; }
        public List<ExpressionType>? Parameters { get; set; }
        public ExpressionType? ReturnType { get; set; }
        public Func<List<object>, Context, object?> Implementation { get; set; }


    }
}
