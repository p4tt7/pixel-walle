using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Lexical;
using pixel_walle.src.Errors;

namespace pixel_walle.src.AST.Expressions.Binary_Operations
{
    public class Equal : BinaryExpression
    {
        private object? value;
        public override ExpressionType Type { get; protected set; }
        public Equal(Expression left, Expression right, CodeLocation location)
            : base(location)
        {
            Left = left;
            Right = right;
            Type = ExpressionType.Bool;
        }

        public override object? Evaluate(Context context, List<Error> errors)
        {
            var leftVal = Left.Evaluate(context, errors);
            var rightVal = Right.Evaluate(context, errors);
            value = Equals(leftVal, rightVal);
            return value;
        }

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            bool leftOk = Left.CheckSemantic(scope, errors);
            bool rightOk = Right.CheckSemantic(scope, errors);

            if (Left.Type != Right.Type)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Operator '==' can't be applied to operands of type {Right.Type} and {Left.Type}", Location));
                return false;
            }

            return leftOk && rightOk;
        }

    }

}
