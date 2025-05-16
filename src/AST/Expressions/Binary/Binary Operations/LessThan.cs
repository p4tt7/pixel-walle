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
    public class LessThan : BinaryExpression
    {

        public LessThan(CodeLocation location) : base(location) { }

        public override ExpressionType Type { get; set; }
        public override object? Value { get; set; }
        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            if (Right.Type != ExpressionType.Number || Left.Type != ExpressionType.Number)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Operator '<' can't be applied to operands of type {Right.Type} and {Left.Type}", Location));
                return false;
            }
            return true;
        }

        public override object? Evaluate()
        {
            Right.Evaluate();
            Left.Evaluate();

            Value = (int)Right.Value > (int)Left.Value;
            return Value;
        }

    }
}
