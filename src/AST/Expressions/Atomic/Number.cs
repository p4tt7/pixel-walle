using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions
{
    public class Number : Atom
    {

        public int? NumberValue { get; private set; }

        public Number(int value, CodeLocation location) : base(location)
        {
            NumberValue = value;
        }

        public override ExpressionType Type => ExpressionType.Number;

        public bool IsInt
        {
            get
            {
                if (NumberValue == null) return false;
                return int.TryParse(NumberValue.ToString(), out _);
            }
        }

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            if (NumberValue is int intValue && intValue <= 0)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, "Los números deben ser mayores que cero.", Location));
                return false;
            }

            return true;
        }

        public override object? Evaluate(Scope scope)
        {
            return NumberValue;
        }
    }


}

