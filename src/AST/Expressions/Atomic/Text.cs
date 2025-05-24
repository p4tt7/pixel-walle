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
        public string TextValue { get; }
        public override ExpressionType Type => ExpressionType.Text;
        public override object? Value => TextValue;


        public Text(CodeLocation location, string value) : base(location)
            {
            TextValue = value;
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
