using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.CodeLocation_;
using pixel_walle.src.AST.Expressions;
using pixel_walle.src.Errors;

namespace pixel_walle.src.AST.Instructions
{

    public class OperationRegistry
    {
        private static readonly Dictionary<string, FunctionInfo> Functions = FunctionInfo.Functions;
        private static readonly Dictionary<string, InstructionInfo> Instructions = InstructionInfo.BuiltIns;

        public static bool TryGetFunction(string name, out FunctionInfo function)
        {
            return Functions.TryGetValue(name, out function);
        }

        public static bool TryGetInstruction(string name, out InstructionInfo instruction)
        {
            return Instructions.TryGetValue(name, out instruction);
        }

        public static List<ExpressionType>? GetParameterTypes(string name)
        {
            if (TryGetFunction(name, out var function))
            {
                return function.ParameterTypes;
            }
            else if (TryGetInstruction(name, out var instruction))
            {
                return instruction.ParameterTypes;
            }
            return null; 
        }
    }



    public class FunctionInfo
    {
        public List<ExpressionType> ParameterTypes { get; }
        public ExpressionType ReturnType { get; }

        public FunctionInfo(List<ExpressionType> parameterTypes, ExpressionType returnType)
        {
            ParameterTypes = parameterTypes;
            ReturnType = returnType;
        }

        public static readonly Dictionary<string, FunctionInfo> Functions = new()
        {
            { "GetColorCount", new FunctionInfo(new List<ExpressionType> { ExpressionType.Number, ExpressionType.Number, ExpressionType.Number } , ExpressionType.Number)},
            { "GetActualX", new FunctionInfo(new List<ExpressionType>(),ExpressionType.Number)},
            { "GetActualY", new FunctionInfo(new List<ExpressionType>(),ExpressionType.Number)},
            { "GetCanvasSize", new FunctionInfo(new List<ExpressionType>(), ExpressionType.Number) },
            { "IsBrushColor", new FunctionInfo(new List<ExpressionType> { ExpressionType.Text}, ExpressionType.Number)  },
            { "IsCanvasColor", new FunctionInfo( new List<ExpressionType> { ExpressionType.Text, ExpressionType.Number, ExpressionType.Number }, ExpressionType.Number)   },
        };



    }



    public class InstructionInfo
    {
        public List<ExpressionType> ParameterTypes { get; }

        public InstructionInfo(List<ExpressionType> parameterTypes)
        {
            ParameterTypes = parameterTypes;
        }

        public static readonly Dictionary<string, InstructionInfo> BuiltIns = new()
    {
        { "Spawn", new InstructionInfo(new() { ExpressionType.Number, ExpressionType.Number }) },
        { "Color", new InstructionInfo(new() { ExpressionType.Text }) },
        { "Size", new InstructionInfo(new() { ExpressionType.Number }) },
        { "DrawLine", new InstructionInfo(new() { ExpressionType.Number, ExpressionType.Number, ExpressionType.Number }) },
        { "DrawCircle", new InstructionInfo(new() { ExpressionType.Number, ExpressionType.Number, ExpressionType.Number }) },
        { "DrawRectangle", new InstructionInfo(new() { ExpressionType.Number, ExpressionType.Number, ExpressionType.Number, ExpressionType.Number }) },
        { "Fill", new InstructionInfo(new()) }
    };

    }

}
