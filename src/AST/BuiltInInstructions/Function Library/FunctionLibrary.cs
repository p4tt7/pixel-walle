using pixel_walle.src.AST.Instructions.Functions;
using pixel_walle.src.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Instructions
{
    public static class FunctionLibrary
    {

        public static Dictionary<string, FunctionInfo> BuiltIns = new()
        {
            ["Spawn"] = new FunctionInfo
            {
                Parameters = new() { ExpressionType.Number, ExpressionType.Number },
                Implementation = new Spawn()
            },

            ["Color"] = new FunctionInfo
            {
                Parameters = new() { ExpressionType.Text },
                Implementation = new Color_()
            },

            ["Size"] = new FunctionInfo
            {
                Parameters = new() { ExpressionType.Number },
                Implementation = new Size()
            },

            ["DrawLine"] = new FunctionInfo
            {
                Parameters = new() { ExpressionType.Number, ExpressionType.Number, ExpressionType.Number },
                Implementation = new DrawLine()
                
            },

            ["DrawRectangle"] = new FunctionInfo
            {
                Parameters = new() { ExpressionType.Number, ExpressionType.Number, ExpressionType.Number, ExpressionType.Number, ExpressionType.Number },
                Implementation = new DrawRectangle()
            },

            ["DrawCircle"] = new FunctionInfo
            {
                Parameters = new() { ExpressionType.Number, ExpressionType.Number, ExpressionType.Number },
                Implementation = new DrawCircle()
               
            },
            ["Fill"] = new FunctionInfo
            {
                Parameters = new(),
                Implementation = new Fill()
            }

        };
    }
}