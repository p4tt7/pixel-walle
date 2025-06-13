using pixel_walle.src.CodeLocation_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static pixel_walle.src.Lexical.Token;

namespace pixel_walle.src.Lexical
{
    public class TokenStream
    {
        private List<Token> tokens;

        private int position;

        public int Position => position;
        public bool End => position >= tokens.Count;


        public TokenStream(List<Token> tokens)
        {
            this.tokens = tokens;
            this.position = 0;
        }

        public Token Peek(int offset = 0)
        {
            int new_position = position + offset;
            if (new_position < 0 || new_position >= tokens.Count)
            {
                return new Token(TokenType.EOF, "", new CodeLocation { File = "EOF", Line = -1, Column = -1 });
            }
            return tokens[new_position];
        }

        public bool Match(TokenType type)
        {
            var token = Peek();
            if (token != null && token.Type == type)
            {
                Advance();  
                return true;
            }
            return false;
        }

        public bool Match(string value)
        {
            var token = Peek();
            if (token != null && token.Value != null && token.Value == value)
            {
                Advance();
                return true;
            }
            return false;
        }

        public Token Advance()
        {
            Token current = Peek(); 
            position++;
            return current;
        }

        public Token Rollback()
        {
            if (position > 0)  
            {
                position--; 
                return tokens[position];  
            }
            return null;
        }



    }



}
