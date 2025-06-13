using pixel_walle.src.CodeLocation_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.Errors;

namespace pixel_walle.src.AST.Expressions
{
    public class LessThanOrEqual : BinaryExpression
    {
        public LessThanOrEqual(Expression left, Expression right, CodeLocation location) : base(location)
        {
            Left = left;
            Right = right;
        }

        public override ExpressionType Type => ExpressionType.Bool;

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            if (Left.Type != ExpressionType.Number || Right.Type != ExpressionType.Number)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Operator '<=' can't be applied to operands of type {Left.Type} and {Right.Type}", Location));
                return false;
            }
            return true;
        }

        public override object? Evaluate(Scope scope, List<Error> errors)
        {
            return (int)Left.Evaluate(scope, errors) <= (int)Right.Evaluate(scope, errors);
        }
    }


}
