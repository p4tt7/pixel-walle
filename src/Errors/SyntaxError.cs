using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.Errors
{
    public class SyntaxError : Exception
    {
        public string ErrorMesssage { get; }
        public string FileName { get; }
        public int Line { get; }
        public int Column { get; }
        public string ErrorCode { get; }



        public SyntaxError(string message, string fileName, int line, int column, string errorCode = null)
        {
            ErrorMesssage = message;
            FileName = fileName;
            Line = line;
            Column = column;
            ErrorCode = errorCode;
        }
    }
}
    