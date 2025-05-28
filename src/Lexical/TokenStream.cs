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
        public bool End => position == tokens.Count - 1;


        public TokenStream(List<Token> tokens)
        {
            this.tokens = tokens;
            this.position = 0;
        }

        public Token Peek(int offset = 0)
        {
            int new_position = position + offset;
            if(new_position < tokens.Count)
            {
                return tokens[new_position];
            }

            return null;

        }

        public bool Match(TokenType type)
        {
            var token = Peek();
            return token != null && token.Type == type;
        }

        public bool Match(string value)
        {
            var token = Peek();
            return token != null && token.Value != null && token.Value == value;
        }

        public Token Advance()
        {
            if (position < tokens.Count)
            {
                return tokens[position++];
            }

            return null;

        }

        public Token Rollback()
        {
            if(position > tokens.Count)
            {
                return tokens[position--];
            }

            return null;
            
        }
        


    }



}
