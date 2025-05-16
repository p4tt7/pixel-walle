using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions.Binary_Operations
{
    public class Pow : BinaryExpression
    {
        public Pow(CodeLocation location) : base(location)
        {
        }

        public override ExpressionType Type { get; set; }
        public override object? Value { get; set; }

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            if (Right.Type != ExpressionType.Number || Left.Type != ExpressionType.Number)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Operator '**' can't be applied to operands of type {Right.Type} and {Left.Type}", Location));
                return false;
            }
            return true;
        }

        public override object? Evaluate()
        {
            return Power((int)Right.Evaluate(), (int)Left.Evaluate());
        }

        public static int Power(int a, int b)
        {
            if (b == 0) return 1;
            if (b == 1) return a;

            int half = Power(a, b / 2);

            if (b % 2 == 0)
                return half * half;
            else
                return half * half * a;
        }

    }
}
