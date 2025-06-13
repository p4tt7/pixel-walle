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
        public bool BoolValue { get; }

        public Bool(CodeLocation location, bool value) : base(location)
        {
            BoolValue = value;
        }

        public override ExpressionType Type => ExpressionType.Bool;


            public override bool CheckSemantic(Scope scope, List<Error> errors)
            {
                return true;
            }

            public override object? Evaluate(Scope scope, List<Error> errors)
            {
                return BoolValue;
            }
        }

    
}
