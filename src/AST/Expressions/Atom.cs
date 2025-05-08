using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.CodeLocation_;

namespace pixel_walle.src.AST.Expressions
{
    public abstract class Atom : Expression
    {
        public Atom(CodeLocation location) : base(location) { }
    }
}
