using pixel_walle.src.CodeLocation_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static pixel_walle.src.Lexical.Token;

namespace pixel_walle.src.AST.Instructions
{
    public abstract class Instruction : ASTNode
    {
        public Instruction(CodeLocation location) : base(location) { }

        public abstract object? Evaluate(Scope scope);
    }
}
