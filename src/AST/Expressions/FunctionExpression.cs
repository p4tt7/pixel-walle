using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace pixel_walle.src.AST.Expressions
{
    public class FunctionExpression : Expression
    {
        public override ExpressionType Type { get; protected set; }
        public string FunctionName { get; set; }
        public List<Expression> Arguments { get; set; } = new();

        public FunctionExpression(string name, List<Expression> arguments, CodeLocation location) : base(location)
        {
            FunctionName = name;
            Arguments = arguments;
        }


        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            if (!FunctionExpressionLibrary.BuiltInsExpressions.TryGetValue(FunctionName, out var functionInfo))
            {
                errors.Add(new Error(Error.ErrorType.Undefined, $"Function '{FunctionName}' is not defined.", Location));
                return false;
            }

            if (Arguments.Count != functionInfo.Parameters.Count)
            {
                errors.Add(new Error(Error.ErrorType.ParameterCountMismatchError,
                    $"Function '{FunctionName}' expects {functionInfo.Parameters.Count} arguments, got {Arguments.Count}.",
                    Location));
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

            if (!allArgsValid)
                return false;

            for (int i = 0; i < Arguments.Count; i++)
            {
                if (Arguments[i].Type != functionInfo.Parameters[i])
                {
                    errors.Add(new Error(Error.ErrorType.ParameterTypeMismatch,
                        $"Argument {i + 1} of '{FunctionName}' must be {functionInfo.Parameters[i]}, but got {Arguments[i].Type}.",
                        Location));
                    return false;
                }
            }

            Type = functionInfo.ReturnType;
            return true;
        }

        public override object? Evaluate(Context context, List<Error> errors)
        {
            if (!FunctionExpressionLibrary.BuiltInsExpressions.TryGetValue(FunctionName, out var functionInfo))
            {
                errors.Add(new Error(Error.ErrorType.Undefined, $"Function '{FunctionName}' is not defined.", Location));
                return null;
            }

            var evaluatedArgs = new List<object?>();
            foreach (var arg in Arguments)
            {
                var value = arg.Evaluate(context, errors);
                evaluatedArgs.Add(value);
            }

            if (errors.Count > 0)
                return null;

            return functionInfo.Implementation.Execute(evaluatedArgs, context, errors, Location);
        }
    }
}
