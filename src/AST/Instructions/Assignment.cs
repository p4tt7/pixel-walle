using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pixel_walle.src.AST.Expressions;
using System.Threading.Tasks;
using pixel_walle.src.Errors;


namespace pixel_walle.src.AST.Instructions
{
    public class Assignment : Expression
    {
        public Assignment(string variableName, Expression expr, CodeLocation location) : base(location)
        {
            VariableName = variableName;
            Expr = expr;
        }

        public string VariableName { get; }
        public Expression Expr { get; }

        public override ExpressionType Type => Expr.Type;
        public override object? Value => Expr.Value;

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

            scope.Define(VariableName, Expr.Type); 

            return true;
        }

        public override object Evaluate()
        {
            object? value = Expr.Evaluate();
            Scope.Current.Define(VariableName, value);
            return null;
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

            foreach (char c in name)
            {
                if (!char.IsLetterOrDigit(c) && c != '_')
                {
                    return false;
                }
            }
            return true;

        }



    }
}
