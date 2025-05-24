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
        private ExpressionType _type;
        private object? _value;

        public int NumberValue { get; }

        public Number(CodeLocation location, int value) : base(location)
        {
            NumberValue = value;
        }

        public override ExpressionType Type => ExpressionType.Number;
        public override object? Value => NumberValue;

        public bool IsInt
        {
            get
            {
                if (Value == null) return false;
                return int.TryParse(Value.ToString(), out _);
            }
        }

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            if (Value is int intValue && intValue <= 0)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, "Los números deben ser mayores que cero.", Location));
                return false;
            }

            return true;
        }

        public override object? Evaluate()
        {
            return Value;
        }
    }


}

