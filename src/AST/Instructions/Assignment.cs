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
            if (!VariableNameValidator(VariableName))
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Nombre de variable inválido: '{VariableName}'", Location));
                return false;
            }

            if (!Expr.CheckSemantic(scope, errors))
            {
                return false;
            }

            scope.Define(VariableName, null, Expr.Type);

            return true;
        }



        public static bool VariableNameValidator(string name)
        {
            if (name == null)
            {
                return false;
            }

            if (Char.IsDigit(name[0]))
            {
                return false;
            }

            if (FunctionLibrary.BuiltIns.ContainsKey(name))
            {
                return false;
            }



            foreach (char c in name)
            {
                if (!char.IsLetterOrDigit(c) && c != '_')
                {
                    return false;
                }
            }
            return true;

        }

        public override object? Evaluate(Context context)
        {
            object value = Expr.Evaluate(context.Scope);
            context.Scope.Define(VariableName, value, Expr.Type);
            return value; 
        }
    }
}
