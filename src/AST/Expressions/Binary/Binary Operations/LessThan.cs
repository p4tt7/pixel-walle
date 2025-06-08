using pixel_walle.src.CodeLocation_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions.Binary.Binary_Operations
{
    public class LessThan : Comparison
    {
        public LessThan(CodeLocation location) : base(location) { }
        protected override string OperatorSymbol => "<";
        protected override bool Compare(object left, object right) => Comparer<object>.Default.Compare(left, right) < 0;
    }
}
