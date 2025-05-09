using pixel_walle.src.CodeLocation_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions.Atomic
{
    public class Function : Expression
    {
        public string FunctionName { get; }
        public List<Expression> Arguments { get; }
        public object Return { get; }

        public Function(string name, List<Expression> arguments, object returnValue, CodeLocation location) : base(location)
        {
            this.FunctionName = name;
            this.Arguments = arguments;
            this.Return = returnValue;
        }

        public override object? Evaluate(Scope scope)
        {
            return null;
        }

        public override bool CheckSemantic(Scope scope)
        {
            return false;
        }

        public Dictionary<string, Function> functions = new Dictionary<string, Function>
        {


        };
    }

}

//       { "GetColorCount",   new FunctionInfo(new List<ParameterType> { ParameterType.Number, ParameterType.Number, ParameterType.Number }, ParameterType.Number) },
//    { "GetActualX",      new FunctionInfo(new List<ParameterType> {}, ParameterType.Number) },
//    { "GetActualY",      new FunctionInfo(new List<ParameterType> {}, ParameterType.Number) },
//    { "GetCanvasSize",   new FunctionInfo(new List<ParameterType> {}, ParameterType.Number) },
//    { "IsBrushColor",    new FunctionInfo(new List<ParameterType> { ParameterType.Color }, ParameterType.Number) },
//    { "IsCanvasColor",   new FunctionInfo(new List<ParameterType> { ParameterType.Color, ParameterType.Number, ParameterType.Number }, ParameterType.Number) }