using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pixel_walle.src.AST.Expressions;
using System.Threading.Tasks;
using pixel_walle.src.Errors;
using System.Xml.Linq;

namespace pixel_walle.src.AST.Instructions
{
    public class FunctionInstruction : Instruction
    {
        public string FunctionName { get; set; }
        public List<Expression> Arguments { get; set; } = new();

        public FunctionInstruction(string name, List<Expression> arguments, CodeLocation location)
            : base(location)
        {
            FunctionName = name;
            Arguments = arguments;
        }


        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            if(!FunctionLibrary.BuiltIns.TryGetValue(FunctionName, out var function))
            {
                errors.Add(new Error(Error.ErrorType.Undefined, $"Function '{FunctionName}' not defined.", Location));
                return false;

            }

            if(Arguments.Count != function.Parameters.Count)
            {
                errors.Add(new Error(Error.ErrorType.ParameterCountMismatchError, $"Function '{FunctionName}' expects {function.Parameters.Count} arguments, got {Arguments.Count}.", Location));
                return false;
            }

            bool allArgsValid = true;
            for (int i = 0; i < Arguments.Count; i++)
            {
                if (!Arguments[i].CheckSemantic(scope, errors))
                {
                    allArgsValid = false;                  
                }
            }

            if (!allArgsValid) return false;

            for (int i = 0; i<Arguments.Count;i++)
            {
                if (Arguments[i].Type != function.Parameters[i])
                {
                    errors.Add(new Error(Error.ErrorType.ParameterTypeMismatch, $"Argument {i + 1} of '{FunctionName}' must be {function.Parameters[i]}, but got {Arguments[i].Type}.", Location));
                    return false;

                }

            }

            return true;

        }

        public override object? Evaluate(Context context, List<Error> errors)
        {
            if (!FunctionLibrary.BuiltIns.TryGetValue(FunctionName, out var function))
            {
                errors.Add(new Error(Error.ErrorType.Undefined, $"Function '{FunctionName}' not defined.", Location));
                return null;
            }

            var evaluatedArgs = new List<object?>();
            foreach (var arg in Arguments)
            {
                var value = arg.Evaluate(context, errors);
                evaluatedArgs.Add(value);
            }

            function.Implementation.Execute(evaluatedArgs, context, errors, Location);

            return null; 
        }

    }
}
