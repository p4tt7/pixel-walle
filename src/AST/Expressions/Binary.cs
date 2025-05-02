using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.CodeLocation_;

namespace pixel_walle.src.AST.Expressions.Binary
{
    public abstract class BinaryExpression : Expression
    {
        public Expression? Right { get; set; }
        public Expression? Left { get; set; }

        public BinaryExpression(Expression left, Expression right, CodeLocation location) : base(location)
        {
            Left = left;
            Right = right;
        }


        public override void CheckSemantic(Scope scope){ }


    }
}
