using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.AST.Instructions;
using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using pixel_walle.src.AST.Expressions;

namespace pixel_walle.src.AST.Instructions
{

    public class Function : Instruction
    {
        public string FunctionName { get; }
        public List<Expression> Arguments { get; }

        public Function(string name, List<Expression> args, CodeLocation location)
            : base(location)
        {
            FunctionName = name;
            Arguments = args;
        }

        public bool IsFunction
        {
            get
            {
                return FunctionInfo.Functions.ContainsKey(FunctionName);

            }
        }

        public override object? Evaluate(Scope scope)
        {
            return null;
        }

        public override bool CheckSemantic(Scope scope)
        {

            var expected = FunctionInfo.Functions[FunctionName].Parameters;
            if (Arguments.Count != expected.Count)
            {
                return false;
            }

            return true;
        }



    }
}
