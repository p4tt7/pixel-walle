using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.CodeLocation_;

namespace pixel_walle.src.AST.Expressions
{
    public abstract class Expression : ASTNode
    {
        public ExpressionType Type { get; protected set; }

        public Expression(CodeLocation location) : base(location) { }

        public abstract object? Evaluate(Scope scope);
    }


    public enum ExpressionType
    {
        Number,
        Text
    }
}

