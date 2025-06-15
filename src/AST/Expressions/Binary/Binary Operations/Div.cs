using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions
{
    public class Div : BinaryExpression
    {
        public override ExpressionType Type { get; protected set; }
        public Div(Expression left, Expression right, CodeLocation location) : base(location)
        {
            Left = left;
            Right = right;
            Type = ExpressionType.Number;
        }


        private object? value;

        public Expression Left { get; }
        public Expression Right { get; }



        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {

            if (!Left.CheckSemantic(scope, errors) || !Right.CheckSemantic(scope, errors))
                return false;

            if (Left.Type != ExpressionType.Number || Right.Type != ExpressionType.Number)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError,
                    $"Operator '/' can't be applied to operands of type {Left.Type} and {Right.Type}",
                    Location));
                return false;
            }

            return true; 
        }

        public override object? Evaluate(Context context, List<Error> errors)
        {

            var leftVal = Left.Evaluate(context, errors);
            var rightVal = Right.Evaluate(context, errors);

            if (leftVal == null || rightVal == null)
                return null;

            if ((int)rightVal == 0)
            {
                errors.Add(new Error(Error.ErrorType.DivisionByZeroError,
                    "Division by zero",
                    Location));
                return null;
            }

            return (int)leftVal / (int)rightVal;
        }
    }
}
