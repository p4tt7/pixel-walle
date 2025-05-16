using System;
using System.Collections.Generic;
using pixel_walle.src.AST.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Instructions
{
    public class InstructionInfo : CallableInfo
    {
        public InstructionInfo(List<ExpressionType> parameters)
            : base(parameters)
        {
        }

        public static readonly Dictionary<string, InstructionInfo> BuiltIns = new()
        {
        { "Spawn",         new InstructionInfo(new List<ExpressionType> { ExpressionType.Number, ExpressionType.Number }) },
        { "Color",         new InstructionInfo(new List<ExpressionType> { ExpressionType.Color }) },
        { "Size",          new InstructionInfo(new List<ExpressionType> { ExpressionType.Number }) },
        { "DrawLine",      new InstructionInfo(new List<ExpressionType> { ExpressionType.Number, ExpressionType.Number, ExpressionType.Number }) },
        { "DrawCircle",    new InstructionInfo(new List<ExpressionType> { ExpressionType.Number, ExpressionType.Number, ExpressionType.Number }) },
        { "DrawRectangle", new InstructionInfo(new List<ExpressionType> { ExpressionType.Number, ExpressionType.Number, ExpressionType.Number, ExpressionType.Number }) },
        { "Fill",          new InstructionInfo(new List<ExpressionType>()) }
        };
    }

}