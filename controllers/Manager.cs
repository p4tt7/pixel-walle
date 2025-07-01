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
using pixel_walle.src.AST.Expressions;


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

        public void PrintAstTree(Expression expr, string indent = "", bool isRight = false)
        {
            if (expr == null) return;

            string branch = indent + (isRight ? "└── " : "├── ");
            Console.WriteLine(branch + expr.GetType().Name);

            // Tipos binarios
            if (expr is Add add)
            {
                PrintAstTree(add.Left, indent + (isRight ? "    " : "│   "), false);
                PrintAstTree(add.Right, indent + (isRight ? "    " : "│   "), true);
            }
            else if (expr is Sub sub)
            {
                PrintAstTree(sub.Left, indent + (isRight ? "    " : "│   "), false);
                PrintAstTree(sub.Right, indent + (isRight ? "    " : "│   "), true);
            }
            else if (expr is Multiplication mul)
            {
                PrintAstTree(mul.Left, indent + (isRight ? "    " : "│   "), false);
                PrintAstTree(mul.Right, indent + (isRight ? "    " : "│   "), true);
            }
            else if (expr is Div div)
            {
                PrintAstTree(div.Left, indent + (isRight ? "    " : "│   "), false);
                PrintAstTree(div.Right, indent + (isRight ? "    " : "│   "), true);
            }
            else if (expr is Pow pow)
            {
                PrintAstTree(pow.Left, indent + (isRight ? "    " : "│   "), false);
                PrintAstTree(pow.Right, indent + (isRight ? "    " : "│   "), true);
            }
            else if (expr is Number num)
            {
                Console.WriteLine(indent + (isRight ? "    " : "│   ") + "└── " + num);
            }
            else if (expr is Variable var)
            {
                Console.WriteLine(indent + (isRight ? "    " : "│   ") + "└── " + var.Name);
            }
        }



    }

}
       
