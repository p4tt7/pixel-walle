using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions
{
    class Negative : Expression
    {
        public override ExpressionType Type { get; protected set; }
        public Expression Operand { get; }

        public Negative(Expression operand, CodeLocation location) : base(location)
        {
            Operand = operand;
            Type = ExpressionType.Number;
        }


        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            var ok = Operand.CheckSemantic(scope, errors);
            if(!ok)
            {
                return false;
            }

            if (Operand.Type != ExpressionType.Number)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"The unary operator '-' is not applicable to an operand of type '{Operand.Type}'.", Location));

                return false;
            }

            return true;
        }

        public override object? Evaluate(Context context, List<Error> errors)
        {
            var val = Operand.Evaluate(context, errors);
            return -(int)val!;
        }
    }
}
