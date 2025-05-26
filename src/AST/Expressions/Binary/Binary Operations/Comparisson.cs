using pixel_walle.src.CodeLocation_;
using System;
using pixel_walle.src.Errors;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions
{
    public class Comparisson : BinaryExpression
    {
        public Comparisson(CodeLocation location) : base(location)
        {
        }

        public override ExpressionType Type { get;}
        public override object? Value { get; }

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            if(Left.Type != Right.Type)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Operator  can't be applied to operands of type {Right.Type} and {Left.Type}", Location));
                return false;   
            }

            return true;
        }

        public override object? Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}
