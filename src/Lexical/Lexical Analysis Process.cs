using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static pixel_walle.src.Lexical.Token;
using pixel_walle.src.Errors;
using System.Windows.Shapes;
using pixel_walle.src.Colores;

namespace pixel_walle.src.Lexical
{
    public class LexicalAnalizer
    {

        public bool MatchOperator(TokenReader stream, List<Token> tokens)
        {
            foreach (var op in operators.Keys.OrderByDescending(k => k.Length))
                if (stream.Match(op))
                {
                    tokens.Add(new Token(TokenType.Symbol, operators[op], stream.Location));
                    return true;
                }
            return false;
        }

        public bool MatchColor(TokenReader stream, List<Token> tokens)
        {
            foreach (var color in Color.colores.Keys.OrderByDescending(k => k.Length))
                if (stream.Match(color))
                {
                    tokens.Add(new Token(TokenType.Color, color, stream.Location));
                    return true;
                }
            return false;

        }


       public List<Token> GetTokens(string fileName, string code, List<Error> errors)
       {
            List<Token> tokens = new List<Token>();
            TokenReader stream = new TokenReader(fileName, code);

            while(!stream.EOF)
            {
                stream.SkipWhitespace();

                string value;

                if (stream.ReadID(out value))
                {
                    TokenType type;

                    if(keywords.ContainsKey(value))
                    {
                        type = TokenType.Keyword;
                    }
                    else
                    {
                        type = TokenType.Identifier;
                    }

                    tokens.Add(new Token(type,value,stream.Location));
                    continue;

                }

                if(stream.ReadNumber(out value))
                {
                    tokens.Add(new Token(TokenType.Number,value,stream.Location));
                    continue;
                }


                if(MatchOperator(stream, tokens))
                {
                    continue;
                }


                char UnknownOP = stream.ReadAny();


                errors.Add(new Error(
                    Error.ErrorType.InvalidTokenError,
                    $"Símbolo no reconocido: '{UnknownOP}'", stream.Location));

            }

            return tokens;


       }

      

    }

   
}
