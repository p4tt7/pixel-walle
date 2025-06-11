using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Lexical;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace pixel_walle.src.AST.Expressions
{
    public class Sub : BinaryExpression
    {

        private object? value;
        public Sub(Expression left, Expression right, CodeLocation location) : base(location)
        {
            Left = left;
            Right = right;
        }

        public override ExpressionType Type => ExpressionType.Number;


        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            if(Right.Type != ExpressionType.Number || Left.Type != ExpressionType.Number)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Operator '-' can't be applied to operands of type {Right.Type} and {Left.Type}", Location));
                return false;
            }
            return true;
            
        }

        public override object? Evaluate(Scope scope)
        {
            Right.Evaluate(scope);
            Left.Evaluate(scope);

            value = (int)Right.Evaluate(scope) - (int)Left.Evaluate(scope);
            return value;
        }
    }
}
