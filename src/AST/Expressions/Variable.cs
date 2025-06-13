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


namespace pixel_walle.src.AST.Expressions
{
    public class Variable : Expression
    {
        public string Name { get; }
        private ExpressionType? resolvedType;

        public override ExpressionType Type => resolvedType.Value;

        public Variable(string name, CodeLocation location) : base(location)
        {
            this.Name = name;

        }

        public override object? Evaluate(Scope scope, List<Error> errors)
        {
            if(scope.GetVariable(Name, out object? variable))
            {
                return variable;
            }

            errors.Add(new Error(Error.ErrorType.Undefined, $"{Name} is not defined in the current context", Location));
            return null;

        }

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            if (!scope.Exists(Name)) 
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Variable '{Name}' not defined", Location));
                return false;
            }

            if (FunctionLibrary.BuiltIns.ContainsKey(Name))
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Cannot assign to {Name} — name is reserved for a built-in function.", Location));

            }

            resolvedType = scope.GetType(Name);
            return true;
        }
    }
}
