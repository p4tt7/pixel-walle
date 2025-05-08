using pixel_walle.src.AST.Expressions;
using pixel_walle.src.Lexical;
using pixel_walle.src.AST.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.Parser
{
    public class Parser
    {

        public TokenStream Stream { get; private set; }

        public void Parser_()
        {
            if (Stream.Peek(0) == null)
            {
                return;
            }

            while(Stream.Advance().Value == TokenType.Number)

        }    

        public Parser(TokenStream stream)
        {
            Stream = stream;
        }

        public Expression? ParseNumber()
        {
            return null;
            
        }

        public bool ParseInstruction()
        {
            return false;
        }


        public Function ParseExpression() 
        {
            return null;
        }




    }

}
