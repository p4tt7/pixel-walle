using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pixel_walle.src.AST.Expressions;
using System.Threading.Tasks;
using pixel_walle.src.Errors;
using pixel_walle.src.AST.Instructions.Functions;
using pixel_walle.src.Lexical;


namespace pixel_walle.src.AST.Instructions
{
    public class Assignment : Instruction
    {
        public Assignment(string variableName, Expression expr, CodeLocation location) : base(location)
        {
            VariableName = variableName;
            Expr = expr;
        }

        public string VariableName { get; }
        public Expression Expr { get; }


        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            if (!VariableNameValidator(VariableName, Location, errors))
            {
                return false;
            }

            if (!Expr.CheckSemantic(scope, errors))
            {
                return false;
            }

            scope.Define(VariableName, null, Expr.Type);
            return true;
        }




        public static bool VariableNameValidator(string name, CodeLocation location, List<Error> errors)
        {
            if (string.IsNullOrEmpty(name))
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Variable name is null or empty.", location));
                return false;
            }

            if (Char.IsDigit(name[0]))
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Variable name cannot start with a digit: '{name}'.", location));
                return false;
            }

            if (FunctionLibrary.BuiltIns.ContainsKey(name))
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Variable name '{name}' conflicts with built-in function.", location));
                return false;
            }

            foreach (char c in name)
            {
                if (!char.IsLetterOrDigit(c) && c != '_')
                {
                    errors.Add(new Error(Error.ErrorType.SemanticError, $"Variable name '{name}' contains invalid character: '{c}'.", location));
                    return false;
                }
            }

            return true;
        }


        public override object? Evaluate(Context context, List<Error> errors)
        {
            object value = Expr.Evaluate(context.Scope, errors);
            context.Scope.Define(VariableName, value, Expr.Type);
            return value; 
        }
    }
}
