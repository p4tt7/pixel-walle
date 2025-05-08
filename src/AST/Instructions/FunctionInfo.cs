using System.Collections.Generic;

namespace pixel_walle.src.AST.Instructions
{
    public class FunctionInfo
    {
        public enum ParameterType
        {
            Number,
            Color,
            Void
        }

        public List<ParameterType> Parameters { get; private set; }

        public ParameterType? ReturnType { get; private set; }

        public FunctionInfo(List<ParameterType> parameters, ParameterType? returnType)
        {
            Parameters = parameters;
            ReturnType = returnType;
        }

        public static Dictionary<string, FunctionInfo> Functions = new Dictionary<string, FunctionInfo>
        {
            { "Spawn",           new FunctionInfo(new List<ParameterType> { ParameterType.Number, ParameterType.Number }, null) },
            { "GetColorCount",   new FunctionInfo(new List<ParameterType> { ParameterType.Number, ParameterType.Number, ParameterType.Number }, ParameterType.Number) },
            { "Color",           new FunctionInfo(new List<ParameterType> { ParameterType.Color }, null) },
            { "Size",            new FunctionInfo(new List<ParameterType> { ParameterType.Number }, ParameterType.Void) },
            { "DrawLine",        new FunctionInfo(new List<ParameterType> { ParameterType.Number, ParameterType.Number, ParameterType.Number }, null) },
            { "DrawCircle",      new FunctionInfo(new List<ParameterType> { ParameterType.Number, ParameterType.Number, ParameterType.Number }, null) },
            { "DrawRectangle",   new FunctionInfo(new List<ParameterType> { ParameterType.Number, ParameterType.Number, ParameterType.Number, ParameterType.Number }, null) },
            { "Fill",            new FunctionInfo(new List<ParameterType> {}, null) },
            { "GetActualX",      new FunctionInfo(new List<ParameterType> {}, ParameterType.Number) },
            { "GetActualY",      new FunctionInfo(new List<ParameterType> {}, ParameterType.Number) },
            { "GetCanvasSize",   new FunctionInfo(new List<ParameterType> {}, ParameterType.Number) },
            { "IsBrushColor",    new FunctionInfo(new List<ParameterType> { ParameterType.Color }, ParameterType.Number) },
            { "IsCanvasColor",   new FunctionInfo(new List<ParameterType> { ParameterType.Color, ParameterType.Number, ParameterType.Number }, ParameterType.Number) }
        };
    }
}
