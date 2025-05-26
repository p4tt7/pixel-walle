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
        public Div(Expression left, Expression right, CodeLocation location) : base(location)
        {
            Left = left;
            Right = right;
        }


        private object? value;

        public Expression Left { get; }
        public Expression Right { get; }

        public override object? Value => value;

        public override ExpressionType Type => ExpressionType.Bool;


        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            if (Right.Type != ExpressionType.Number || Left.Type != ExpressionType.Number)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Operator '/' can't be applied to operands of type {Right.Type} and {Left.Type}", Location));
                return false;
            }

            if((int)Right.Value == 0)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Division by 0 is not allowed", Location));
                return false;
            }

            return true;
        }

        public override object? Evaluate()
        {
            Right.Evaluate();
            Left.Evaluate();

            value = (int)Right.Evaluate() / (int)Left.Evaluate();
            return value;
        }
    }
}
