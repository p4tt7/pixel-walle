using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.CodeLocation_;

namespace pixel_walle.src.AST.Expressions
{
    public abstract class BinaryExpression : Expression
    {
        public Expression? Right { get; set; }
        public Expression? Left { get; set; }

        public BinaryExpression(CodeLocation location) : base(location)
        {
        }


    }
}
