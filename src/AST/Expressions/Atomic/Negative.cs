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
        public Expression Operand { get; }

        public Negative(Expression operand, CodeLocation location) : base(location)
        {
            Operand = operand;
        }

        public override ExpressionType Type => Operand.Type;

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            var ok = Operand.CheckSemantic(scope, errors);
            if(!ok)
            {
                return false;
            }

            if (Operand.Type != ExpressionType.Number)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"El operador unario '-' no se puede aplicar a un operando de tipo '{Operand.Type}'.", Location));
                return false;
            }

            return true;
        }

        public override object? Evaluate(Scope scope, List<Error> errors)
        {
            var val = Operand.Evaluate(scope, errors);
            return -(int)val!;
        }
    }
}
