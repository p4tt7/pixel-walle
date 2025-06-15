using pixel_walle.src.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions
{
    public class FunctionExpressionInfo
    {
        public List<ExpressionType> Parameters { get; set; } = new();
        public ExpressionType ReturnType { get; set; }
        public IFunction Implementation { get; set; }
    }
}
