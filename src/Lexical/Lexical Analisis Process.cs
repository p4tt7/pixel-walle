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

namespace pixel_walle.src.Lexical
{
    public class LexicalAnalizer
    {
        Dictionary<string, string> operators = new Dictionary<string, string>();
        Dictionary<string, string> keywords = new Dictionary<string, string>();

        public void RegisterOperator(string op, string token_value)
        {
            this.operators[op] = token_value;
        }

        public void RegisterKeyword(string keyword, string token_value)
        {
            this.keywords[keyword] = token_value;
        }

        private bool MatchSymbol(TokenReader stream, List<Token> tokens)
        {
            foreach (var op in operators.Keys.OrderByDescending(k => k.Length))
                if (stream.Match(op))
                {
                    tokens.Add(new Token(TokenType.Symbol, operators[op], stream.Location));
                    return true;
                }
            return false;
        }

       public List<Token> GetTokens(string fileName, string code, List<SyntaxError> errors)
       {
            List<Token> tokens = new List<Token>();
            TokenReader stream = new TokenReader(fileName, code);

            while(!stream.EOF)
            {
                stream.SkipWhitespace();

                string value;

                if (stream.ReadID(out value))
                {
                    TokenType type = keywords.ContainsKey(value) ? TokenType.Keyword : TokenType.Identifier;
                    tokens.Add(new Token(type,value,stream.Location));
                    continue;

                }

                if(stream.ReadNumber(out value))
                {
                    tokens.Add(new Token(TokenType.Number,value,stream.Location));
                    continue;
                }

                if(MatchSymbol(stream, tokens))
                {
                    continue;
                }

                char UnknownOP = stream.ReadAny();
                errors.Add(new SyntaxError());

            }

            return tokens;


       }

    

       

    }

   
}
