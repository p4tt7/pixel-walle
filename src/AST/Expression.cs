using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;

namespace pixel_walle.src.AST.Expressions
{
    public abstract class Expression : ASTNode
    {
        public abstract ExpressionType Type { get; protected set; }

        public Expression(CodeLocation location) : base(location) { }

        public abstract object? Evaluate(Context context, List<Error> errors);
    }


    public enum ExpressionType
    {
        Number,
        Text,
        Bool
    }
}

