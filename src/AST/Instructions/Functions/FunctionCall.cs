using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Instructions.Functions
{
    public class FunctionCall : Instruction
    {
        public FunctionCall(CodeLocation location) : base(location)
        {
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
