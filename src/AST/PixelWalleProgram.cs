using pixel_walle.src.AST.Instructions;
using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST
{
    public class PixelWalleProgram : ASTNode
    {
        public List<Instruction> Instructions { get; }

        public PixelWalleProgram(List<Instruction> instructions, CodeLocation location) : base(location)
        {
            Instructions = instructions;
        }

        public object? Evaluate(Context context)
        {
            foreach (var instr in Instructions)
            {
                instr.Evaluate(context); 
            }

            return null;
        }

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            foreach(var instruction in Instructions)
            {
                if(!instruction.CheckSemantic(scope, errors))
                {
                    return false;
                }
            }

            return true;
        }
    }

}
