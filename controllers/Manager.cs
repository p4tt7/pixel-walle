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


namespace pixel_walle.controllers
{
        public class Manager
        {

            public string FileName { get; set; }

            private LexicalAnalizer lexer = new LexicalAnalizer();

            public string SourceCode { get; private set; }
            public List<Error> Errors { get; private set; }

            public Manager(string fileName, string sourceCode)
            {
                FileName = fileName;
                SourceCode = sourceCode;
                Errors = new List<Error>();
            }

            public void Analyze()
            {

                List<Token> tokens = lexer.GetTokens(FileName, SourceCode, Errors);
                TokenStream stream = new TokenStream(tokens);
                Parser parser = new Parser(stream);
                List<Error> parseErrors = new List<Error>();
                ASTNode? ast = parser.Parse(parseErrors);

                Errors.AddRange(parseErrors);


        }

        }

}
       
