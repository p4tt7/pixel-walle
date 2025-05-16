using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.AST.Expressions;

namespace pixel_walle.src.AST
{
    public abstract class CallableInfo
    {
        public List<ExpressionType> Parameters { get; }

        protected CallableInfo(List<ExpressionType> parameters)
        {
            Parameters = parameters;
        }
    }
}
