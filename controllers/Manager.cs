using pixel_walle.src.Errors;
using pixel_walle.src.Parser;
using System;
using System.Collections.Generic;
using pixel_walle.src.Lexical;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using pixel_walle.src.AST;
using pixel_walle.src;


namespace pixel_walle.controllers
{
        public class Manager
        {

             public Context Context { get; private set; }

            public string FileName { get; set; }

            private LexicalAnalizer lexer = new LexicalAnalizer();

            public string SourceCode { get; private set; }
            public List<Error> Errors { get; private set; }

            public Manager(string fileName, string sourceCode, int canvasWidth, int canvasHeight)
            {
                FileName = fileName;
                SourceCode = sourceCode;           
                Errors = new List<Error>();
                Context = new Context(new Scope(), canvasWidth, canvasHeight);
            }

        public void Analyze()
        {

            List<Token> tokens = lexer.GetTokens(FileName, SourceCode, Errors);
            TokenStream stream = new TokenStream(tokens);
            Parser parser = new Parser(stream);
            List<Error> parseErrors = new List<Error>();
            PixelWalleProgram? program = parser.Parse(parseErrors);

            if (program != null && Errors.Count == 0)
            {
                program.Evaluate(Context); 
            }

            Errors.AddRange(parseErrors);
        }

        }

}
       
