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

        public bool Compile(out PixelWalleProgram? program)
        {
            List<Token> tokens = lexer.GetTokens(FileName, SourceCode, Errors);

            if (Errors.Any(e =>
                e.Type == Error.ErrorType.LexicalError ||
                e.Type == Error.ErrorType.InvalidTokenError))
            {
                program = null;
                return false;
            }

            TokenStream stream = new TokenStream(tokens);
            Parser parser = new Parser(stream);

            List<Error> compilingErrors = new List<Error>();
            program = parser.Parse(compilingErrors);
            Errors.AddRange(compilingErrors);

            if (program != null)
            {
                List<Error> semanticErrors = new List<Error>();
                if (!program.CheckSemantic(Context.Scope, semanticErrors))
                    Errors.AddRange(semanticErrors);

                if (Errors.Count == 0)
                {
                    return true;
                }
            }

            return false;
        }


    }

}
       
