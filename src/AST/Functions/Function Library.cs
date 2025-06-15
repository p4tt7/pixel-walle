
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions
{
    public class FunctionExpressionLibrary
    {

        public static Dictionary<string, FunctionExpressionInfo> BuiltInsExpressions { get; } = new()
        {
            ["GetActualX"] = new FunctionExpressionInfo
            {
                Parameters = new(),
                ReturnType = ExpressionType.Number,
                Implementation = new GetActualX()
            },

            ["GetActualY"] = new FunctionExpressionInfo
            {
                Parameters = new(),
                ReturnType = ExpressionType.Number,
                Implementation = new GetActualY()
            },

            ["IsCanvasColor"] = new FunctionExpressionInfo
            {
                Parameters = new() { ExpressionType.Text, ExpressionType.Number, ExpressionType.Number },
                ReturnType = ExpressionType.Number,
                Implementation = new IsCanvasColor()
            },

            ["IsBrushColor"] = new FunctionExpressionInfo
            {
                Parameters = new() { ExpressionType.Text },
                ReturnType = ExpressionType.Number,
                Implementation = new IsBrushColor()
            },

            ["IsBrushSize"] = new FunctionExpressionInfo
            {
                Parameters = new() { ExpressionType.Number },
                ReturnType = ExpressionType.Number,
                Implementation = new IsBrushSize()
            },

            ["GetColorCount"] = new FunctionExpressionInfo
            {
                Parameters = new() {
                ExpressionType.Text,
                ExpressionType.Number,
                ExpressionType.Number,
                ExpressionType.Number,
                ExpressionType.Number
            },
                ReturnType = ExpressionType.Number,
                Implementation = new GetColorCount()
            },
        };

    }
}
