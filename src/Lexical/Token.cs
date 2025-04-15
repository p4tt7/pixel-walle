using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        public class CodeLocation
        {
            public string File;
            public int Line;
            public int Column;

        }

        public enum TokenType
        {
            Unknown,
            Number,
            Text,
            Keyword,
            Identifier,
            Symbol,
            EOF, //end of file
            Newline

        }

    }

    public class TokenValues
    {
        protected TokenValues() { }

        public const string Add = "Add";
        public const string Sub = "Subtraction";
        public const string Mul = "Multiplication";
        public const string Div = "Division";
        public const string Assign = "Assign";
        public const string And = "And";
        public const string Or = "Or";
        public const string LessThan = "LessThan";
        public const string LessOrEqualThan = "LessOrEqualThan";
        public const string GreaterThan = "GreaterThan";
        public const string GreaterOrEqualThan = "GreaterOrEqualThan";
        public const string Pow = "Pow";
        public const string Mod = "Modulo";
        public const string GoTo = "GoTo";
        public const string Comma = "Comma";
        public const string OpenBracket = "OpenBracket";
        public const string CloseBracket = "CloseBracket";
        public const string OpenCurlyBraces = "OpenCurlyBraces";
        public const string ClosedCurlyBraces = "ClosedCurlyBraces";

        
    }
}
