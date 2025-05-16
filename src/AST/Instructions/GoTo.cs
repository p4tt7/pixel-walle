using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pixel_walle.src.CodeLocation_;
using System.Threading.Tasks;
using pixel_walle.src.Errors;

namespace pixel_walle.src.AST.Instructions
{
    public class GoTo : Instruction
    {
        public string Label;

        public GoTo(string label, CodeLocation location) : base(location)
        {
            Label = label;
        }

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            throw new NotImplementedException();
        }

        public override object? Evaluate(Scope scope)
        {
            throw new NotImplementedException();
        }
    }
}
