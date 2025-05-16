using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.AST.Instructions;
using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using pixel_walle.src.AST.Expressions;

namespace pixel_walle.src.AST.Instructions
{

    public class BuiltInFunction : Expression
    {
        public string FunctionName { get; }
        public List<Expression> Arguments { get; }

        public BuiltInFunction(string name, List<Expression> args, CodeLocation location)
            : base(location)
        {
            FunctionName = name;
            Arguments = args;
        }

        public bool Exists => FunctionInfo.BuiltIns.ContainsKey(FunctionName);

        public override ExpressionType Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override object? Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override object? Evaluate()
        {
            return null;
        }

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            return false;

        }
    }

}

