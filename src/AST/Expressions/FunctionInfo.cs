using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions
{
    public class FunctionInfo : CallableInfo
    {
        public ExpressionType ReturnType { get; }

        public FunctionInfo(List<ExpressionType> parameters, ExpressionType returnType)
            : base(parameters)
        {
            ReturnType = returnType;
        }

        public static readonly Dictionary<string, FunctionInfo> BuiltIns = new()
    {
        { "GetColorCount", new FunctionInfo(new() { ExpressionType.Number, ExpressionType.Number, ExpressionType.Number }, ExpressionType.Number) },
        { "GetActualX",    new FunctionInfo(new(), ExpressionType.Number) },
        { "GetActualY",    new FunctionInfo(new(), ExpressionType.Number) },
        { "GetCanvasSize", new FunctionInfo(new(), ExpressionType.Number) },
        { "IsBrushColor",  new FunctionInfo(new() { ExpressionType.Color }, ExpressionType.Number) },
        { "IsCanvasColor", new FunctionInfo(new() { ExpressionType.Color, ExpressionType.Number, ExpressionType.Number }, ExpressionType.Number) }
    };
    }

}
