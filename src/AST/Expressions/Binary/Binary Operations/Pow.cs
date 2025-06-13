using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions
{
    public class Pow : BinaryExpression
    {
        private object? value;

        public Pow(Expression left, Expression right, CodeLocation location) : base(location)
        {
            Left = left;
            Right = right;
        }

        public Expression Left { get; private set; }
        public Expression Right { get; private set; }

        public override ExpressionType Type => ExpressionType.Number;


        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            if (Right.Type != ExpressionType.Number || Left.Type != ExpressionType.Number)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Operator '**' can't be applied to operands of type {Right.Type} and {Left.Type}", Location));
                return false;
            }
            return true;
        }

        public override object? Evaluate(Scope scope, List<Error> errors)
        {
            value = Power((int)Right.Evaluate(scope, errors), (int)Left.Evaluate(scope, errors));
            return value;
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
