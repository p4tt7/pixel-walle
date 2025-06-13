using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace pixel_walle.src.AST.Instructions
{
    public class Label : Instruction
    {
        public string Name { get; set; }

        public List<Instruction> Body { get; set; } = new();
        public Label(string name, CodeLocation location) : base(location)
        {
            Name = name;
        }

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            if (string.IsNullOrWhiteSpace(Name) || char.IsDigit(Name[0]))
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, $"Invalid name label: {Name}", Location));
                return false;
            }

            if (scope.Labels.ContainsKey(Name))
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, $"Invalid name label: {Name}", Location));
                return false;
            }

            scope.Labels[Name] = this;
            return true;

        }

        public override object? Evaluate(Context context, List<Error> errors)
        {
            foreach (var instr in Body)
            {
                instr.Evaluate(context, errors);
            }

            return null;
        }
    }
}
