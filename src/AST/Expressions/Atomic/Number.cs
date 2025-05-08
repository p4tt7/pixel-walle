using pixel_walle.src.CodeLocation_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions
{
    public class Number : Atom
    {
        public int Value {  get; private set; }

        public Number(CodeLocation location, int value) : base(location) 
        {
            Value = value;
            Type = ExpressionType.Number;

        }

        public bool IsInt
        {
            get
            {
                int a;
                return int.TryParse(Value.ToString(), out a);
            }
        }

        public override bool CheckSemantic(Scope scope)
        {

            if(Value <= 0)
            {
                return false;
            }

            return true;
            

        }
        public override object? Evaluate(Scope scope)
        {
            return Value;
        }



    }
}
