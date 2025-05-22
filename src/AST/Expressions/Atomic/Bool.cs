using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions.Atomic
{

        public class Bool : Atom
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

            public Bool(CodeLocation location, bool value) : base(location)
            {
                Value = value;
                Type = ExpressionType.Bool;
            }

            public override bool CheckSemantic(Scope scope, List<Error> errors)
            {
                return true;
            }

            public override object? Evaluate()
            {
                return Value;
            }
        }

    
}
