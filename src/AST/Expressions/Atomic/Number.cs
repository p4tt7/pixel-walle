using pixel_walle.src.CodeLocation_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions.Atomic
{
    public class Number : Atom
    {
        public int Value {  get; private set; }

        public Number(CodeLocation location, int value) : base(location) 
        {
            Value = value;
            Type = ExpressionType.Number;

        }

        public override void CheckSemantic(Scope scope)
        {

        }
        public override object? Evaluate(Scope scope)
        {
            return Value;
        }



    }
}
