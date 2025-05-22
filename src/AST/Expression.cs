using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.CodeLocation_;

namespace pixel_walle.src.AST.Expressions
{
    public abstract class Expression : ASTNode
    {
        public abstract ExpressionType Type { get; set; }

        public Expression(CodeLocation location) : base(location) { }
        public abstract object? Value { get; set; }

        public abstract object? Evaluate();
    }


    public enum ExpressionType
    {
        Number,
        Text,
        Bool
    }
}

