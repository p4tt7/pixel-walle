using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace pixel_walle.controllers
{
    public class Analyzer
    {

        public string SourceCode { get; private set; }
        public List<Error> Errors { get; private set; }

        public Analyzer(string sourceCode)
        {
            SourceCode = sourceCode;
            Errors = new List<Error>();
        }

        public void Analyze()
        {
            //lexer

            //parser

            //error semantico
        }

    }
}
