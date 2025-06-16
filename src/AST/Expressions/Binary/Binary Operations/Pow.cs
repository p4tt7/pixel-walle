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
        public override ExpressionType Type { get; protected set; }

        public Pow(Expression left, Expression right, CodeLocation location) : base(location)
        {
            Left = left;
            Right = right;
            Type = ExpressionType.Number;
        }

        public Expression Left { get; private set; }
        public Expression Right { get; private set; }


        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            bool leftValid = Left.CheckSemantic(scope, errors);
            bool rightValid = Right.CheckSemantic(scope, errors);

            if (!leftValid || !rightValid)
                return false;

            if (Right.Type != ExpressionType.Number || Left.Type != ExpressionType.Number)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Operator '**' can't be applied to operands of type {Right.Type} and {Left.Type}", Location));
                return false;
            }
            return true;
        }

        public override object? Evaluate(Context context, List<Error> errors)
        {
            value = Power((int)Left.Evaluate(context, errors), (int)Right.Evaluate(context, errors));
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
