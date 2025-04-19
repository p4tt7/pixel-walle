using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.Errors
{
    public class SyntaxError : Error
    {

        public SyntaxError(string fileName, int line, int column)
            : base($"Syntax error at {fileName}({line},{column})", fileName, line, column)
        {
        }

    }
}
    