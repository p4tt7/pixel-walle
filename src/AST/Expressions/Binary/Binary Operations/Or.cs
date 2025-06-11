using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Lexical;
using pixel_walle.src.Errors;

namespace pixel_walle.src.AST.Expressions
{
    public class Or : BinaryExpression
    {

        private object? value;

        public Or(Expression left, Expression right, CodeLocation location) : base(location)
        {
            Left = left;
            Right = right;
        }

        public override ExpressionType Type => ExpressionType.Bool;

        public override bool CheckSemantic(Scope scope, List<Error> errors)
            {
                if (Right.Type != ExpressionType.Bool || Left.Type != ExpressionType.Bool)
                {
                    errors.Add(new Error(Error.ErrorType.SemanticError, $"Operator '||' can't be applied to operands of type {Right.Type} and {Left.Type}", Location));
                    return false;
                }
                return true;
            }

            public override object? Evaluate(Scope scope)
            {
                Right.Evaluate(scope);
                Left.Evaluate(scope);

                value = (bool)Right.Evaluate(scope) || (bool)Left.Evaluate(scope);
                return value;
            }

    }
}
