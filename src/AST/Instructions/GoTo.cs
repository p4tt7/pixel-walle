using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pixel_walle.src.AST.Expressions;
using pixel_walle.src.CodeLocation_;
using System.Threading.Tasks;
using pixel_walle.src.Errors;

namespace pixel_walle.src.AST.Instructions
{
    public class GoTo : Instruction
    {
        public Instruction Label;
        public Expression Condition {  get; set; }
        public List<Instruction> Body { get; set; }
        public int Executions { get; set; } = 0;
        public const int MaxExecutions = 1000;

        public GoTo(Instruction label, Expression condition, CodeLocation location) : base(location)
        {
            Label = label;
            Condition = condition;
        }

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            throw new NotImplementedException();
        }

        public override object? Evaluate(Context context)
        {
            throw new NotImplementedException();
        }

    }
}
