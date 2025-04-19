using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.Errors
{
    public abstract class Error : Exception
    {
        public string FileName { get; protected set; }
        public int Line { get; protected set; }
        public int Column { get; protected set; }

        public Error(string message, string fileName = "", int line = 0, int column = 0)
            : base(message)
        {
            FileName = fileName;
            Line = line;
            Column = column;
        }


    }
}
