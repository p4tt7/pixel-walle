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
        public override ExpressionType Type { get; protected set; }
        public int? NumberValue { get; private set; }

        public Number(int value, CodeLocation location) : base(location)
        {
            NumberValue = value;
            Type = ExpressionType.Number;
        }





        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            return true;
        }

        public override object? Evaluate(Context context, List<Error> errors)
        {
            return NumberValue;
        }
        public override bool TryEvaluateConstant(out object? constantValue)
        {
            constantValue = NumberValue;
            return true;
        }


    }


}

