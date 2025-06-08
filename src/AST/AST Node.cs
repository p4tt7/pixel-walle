using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.Errors;
using static pixel_walle.src.Lexical.Token;
using pixel_walle.src.CodeLocation_;


namespace pixel_walle.src.AST
{
    public abstract class ASTNode
    {
        public CodeLocation Location { get; private set; }


        public ASTNode(CodeLocation locantion)
        {
            Location = locantion;

        }

        public abstract bool CheckSemantic(Scope scope, List<Error> errors);
    }
}
