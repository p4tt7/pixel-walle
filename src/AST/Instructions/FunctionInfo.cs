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

        public FunctionInfo(List<ParameterType> parameters)
        {
            Parameters = parameters;
        }

        public static Dictionary<string, FunctionInfo> Built_In_Functions = new Dictionary<string, FunctionInfo>
        {
            { "Spawn",           new FunctionInfo(new List<ParameterType> { ParameterType.Number, ParameterType.Number }) },
            { "Color",           new FunctionInfo(new List<ParameterType> { ParameterType.Color })},
            { "Size",            new FunctionInfo(new List<ParameterType> { ParameterType.Number })},
            { "DrawLine",        new FunctionInfo(new List<ParameterType> { ParameterType.Number, ParameterType.Number, ParameterType.Number }) },
            { "DrawCircle",      new FunctionInfo(new List<ParameterType> { ParameterType.Number, ParameterType.Number, ParameterType.Number }) },
            { "DrawRectangle",   new FunctionInfo(new List<ParameterType> { ParameterType.Number, ParameterType.Number, ParameterType.Number, ParameterType.Number }) },
            { "Fill",            new FunctionInfo(new List<ParameterType> { } ) },

        };    
            
          
        
    }
}
