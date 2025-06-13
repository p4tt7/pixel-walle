using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Shapes;
using pixel_walle.src.CodeLocation_;

namespace pixel_walle.src.Lexical
{
    public class Token
    {
        public string Value { get; private set; }
        public TokenType Type { get; private set; }
        public CodeLocation Location { get; private set; }


        public Token(TokenType type, string value, CodeLocation location)
        {
            this.Type = type;
            this.Value = value;
            this.Location = location;
        }

        public override string ToString()
        {
            return $"{Type}:{Value}:{Location}";
        }


        public readonly static Dictionary<string, string> operators = new Dictionary<string, string>
        {
            { "+", TokenValue.Add },
            { "-", TokenValue.Sub },
            { "*", TokenValue.Mul },
            { "/", TokenValue.Div },
            { "**", TokenValue.Pow },
            { "%", TokenValue.Mod },
            { "<", TokenValue.LessThan },
            { ">", TokenValue.GreaterThan },
            { "<=", TokenValue.LessOrEqualThan },
            { ">=", TokenValue.GreaterOrEqualThan },
            { "==", TokenValue.Equal },
            {"," , TokenValue.Comma },
            { "\"", TokenValue.QuoteMark },
            {"(", TokenValue.OpenRoundBracket },  
            {")", TokenValue.CloseRoundBracket },
            { "}", TokenValue.ClosedCurlyBraces },
            { "{", TokenValue.OpenCurlyBraces },
            {"[", TokenValue.OpenSquareBracket },
            {"]", TokenValue.ClosedSquareBracket },
            { "<-", TokenValue.Assign }
        };

        public readonly static Dictionary<string, string> keywords = new Dictionary<string, string>
        {
            {"GoTo" , TokenValue.GoTo }

        };

        public readonly static Dictionary<string, string> built_in_functions = new Dictionary<string, string>
        {
            {"Spawn" , TokenValue.Spawn },
            {"Size" , TokenValue.Size },
            {"DrawLine",TokenValue.DrawLine },
            {"DrawCircle",TokenValue.DrawCircle },
            {"DrawRectangule",TokenValue.DrawRectangle },
            {"Fill", TokenValue.Fill },
            {"GetActualX" , TokenValue.GetActualX },
            {"GetActualY", TokenValue.GetActualY },
            {"GetCanvaSize",TokenValue.GetCanvasSize },
            {"IsBrushColor", TokenValue.IsBrushColor },
            {"IsBrushSize",TokenValue.IsBrushSize },
            {"IsCanvasColor",TokenValue.IsBrushSize},

        };



    }
}
