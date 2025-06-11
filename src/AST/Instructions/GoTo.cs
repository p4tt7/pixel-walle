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
            if(Condition.CheckSemantic(scope, errors) && Condition.Type!=ExpressionType.Bool)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Condition must be boolean.", Condition.Location));
                return false;
            }

            if (Label is not Label label)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"The destination instruction in GoTo is not a valid label.", Label.Location));
                return false;
            }

            if (!scope.Labels.TryGetValue(label.Name, out var resolvedLabel))
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Label '{label.Name}' is not defined in the actual scope.", Label.Location));
                return false;
            }

            Body = ((Label)resolvedLabel).Body;
            return true;


        }

        public override object? Evaluate(Context context)
        {
            var cond = Condition.Evaluate(context.Scope);
            bool b;
            if (cond is bool temp)
            {
                b = temp;
                if (!b)
                    return null;
            }
                
            return null;
           

        }

    }
}
