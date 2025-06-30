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
        public override ExpressionType Type { get; protected set; }
        public string TextValue { get; }


        public Text(CodeLocation location, string value) : base(location)
        {
            TextValue = value;
            Type = ExpressionType.Text;

        }
        public override bool TryEvaluateConstant(out object? constantValue)
        {
            constantValue = TextValue;
            return true;
        }


        public override bool CheckSemantic(Scope scope, List<Error> errors)
            {
                if (string.IsNullOrEmpty(TextValue?.ToString()))
                {
                errors.Add(new Error(Error.ErrorType.SemanticError, "String literals must not be empty.", Location));

                return false;
                }

                return true;
            }

            public override object? Evaluate(Context context, List<Error> errors)
            {
                return TextValue;
            }
        }

}
