using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions
{
        public class Text : Atom
        {
            private ExpressionType _type;
            private object? _value;

            public override ExpressionType Type
            {
                get => _type;
                set => _type = value;
            }

            public override object? Value
            {
                get => _value;
                set => _value = value;
            }

            public Text(CodeLocation location, string value) : base(location)
            {
                Value = value;
                Type = ExpressionType.Text; 
            }

            public override bool CheckSemantic(Scope scope, List<Error> errors)
            {
                if (string.IsNullOrEmpty(Value?.ToString()))
                {
                    errors.Add(new Error(Error.ErrorType.SemanticError, "Los textos no pueden estar vacíos.", Location));
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
