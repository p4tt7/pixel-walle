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
        public string LabelName { get; }
        public Expression Condition {  get; }

        public GoTo(string labelName, Expression condition, CodeLocation location) : base(location)
        {
            LabelName = labelName;
            Condition = condition;
        }

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            if (Condition.CheckSemantic(scope, errors) && Condition.Type != ExpressionType.Bool)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Condition must be boolean.", Condition.Location));
                return false;
            }

            if (!scope.Labels.TryGetValue(LabelName, out var label))
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Label '{LabelName}' is not defined in the actual scope.", Location));
                return false;
            }

            return true;


        }

        public override object? Evaluate(Context context, List<Error> errors)
        {
            var result = Condition.Evaluate(context, errors);

            if (result is bool b && b)
            {
                return LabelName;
            }

            return null;

        }

    }
}
