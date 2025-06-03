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
            Executions = 0;

            while (true)
            {
                if (Executions++ > MaxExecutions)
                    throw new Exception($"GoTo exceeded maximum allowed executions ({MaxExecutions}). Possible infinite loop.");

                foreach (var instruction in Body)
                    instruction.Evaluate(context);

                var conditionResult = Condition?.Evaluate(context);
                if (conditionResult is bool result && result)
                {
                    if (Label is Label labelInstr)
                    {
                        context.Scope.Labels(labelInstr.Name); 
                        return null;
                    }
                    else
                    {
                        throw new Exception("GoTo label is not a valid LabelInstruction.");
                    }
                }
            }
        }

    }
}
