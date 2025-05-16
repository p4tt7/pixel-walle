using pixel_walle.src.AST.Expressions;
using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions
{
    public abstract class Function : Expression
    {
        public string FunctionName { get; }
        public List<Expression> Arguments { get; }
        public ExpressionType ReturnType { get; }


        public override ExpressionType Type { get; set; }

        public override object? Value { get; set; }

        public Function(string name, List<Expression> arguments, ExpressionType returnValue, CodeLocation location) : base(location)
        {
            FunctionName = name;
            Arguments = arguments;
            ReturnType = returnValue;
        }

        public override object? Evaluate()
        {
            return null;
        }

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            return false;
        }



        public static readonly Dictionary<string, FunctionInfo> Functions = new()
       {
       { "GetColorCount",   new FunctionInfo(new List<ExpressionType> { ExpressionType.Number, ExpressionType.Number, ExpressionType.Number }, ExpressionType.Number) },
       { "GetActualX",      new FunctionInfo(new List<ExpressionType> {}, ExpressionType.Number) },
       { "GetActualY",      new FunctionInfo(new List<ExpressionType> {}, ExpressionType.Number) },
       { "GetCanvasSize",   new FunctionInfo(new List<ExpressionType> {}, ExpressionType.Number) },
       { "IsBrushColor",    new FunctionInfo(new List<ExpressionType> { ExpressionType.Color }, ExpressionType.Number) },
       { "IsCanvasColor",   new FunctionInfo(new List<ExpressionType> { ExpressionType.Color, ExpressionType.Number, ExpressionType.Number }, ExpressionType.Number) }
       };

    }
}