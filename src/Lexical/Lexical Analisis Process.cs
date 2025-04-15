using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static pixel_walle.src.Lexical.Token;

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

    }

   
}
