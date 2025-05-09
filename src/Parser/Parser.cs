using pixel_walle.src.AST.Expressions;
using pixel_walle.src.Lexical;
using pixel_walle.src.AST.Instructions;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace pixel_walle.src.Parser
{
    public class Parser
    {

        public TokenStream Stream { get; private set; } 




        public Parser(TokenStream stream)
        {
            Stream = stream;
        }



        public Expression? ParseNumber()
        {
            Token token = Stream.Advance();

            if (token.Type != TokenType.Number)
                return null;

            int number = int.Parse(token.Value.ToString());

            return new Number(token.Location, number);


        }





        public object ParseStatement(List<Error> errors)
        {

            Token token = Stream.Advance();

            if(!VariableNameValidator(token.Value.ToString()))
            {
                errors.Add(new Error(Error.ErrorType.AssignmentError, $"Invalid variable name" , Stream.Advance().Location));
            }

            Token assignToken = Stream.Advance();

            if (assignToken.Value != TokenValue.Assign)
            {
                errors.Add(new Error(Error.ErrorType.AssignmentError, $"Invalid assignation symbol, {TokenValue.Assign} expected", Stream.Advance().Location));

            }

            return ParseExpression(errors);  
            
        }






        public Instruction ParseInstruction(List<Error> errors)
        {
            Token token = Stream.Peek();

            if (token.Type == TokenType.Identifier)
            {
                Token next = Stream.Peek(1);

                if (next.Value == TokenValue.Assign)
                {
                    return ParseAssignment(errors);
                }
                else
                {
                    return ParseFunctionCall(errors);
                }
            }

            if (token.Value == TokenValue.GoTo)
            {
                return ParseGoto(errors);
            }

            errors.Add(new Error(Error.ErrorType.SyntaxError, "Unknown instruction", token.Location));
            return null;
        }












        private Assignment ParseAssignment(List<Error> errors)
        {
            Token nameToken = Stream.Advance(); 
            string varName = nameToken.Value.ToString();

            Token assignToken = Stream.Advance();

            if (assignToken.Value != TokenValue.Assign)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "'=' expected", assignToken.Location));
            }

            Expression expr = ParseExpression(errors);

            return new Assignment(assignToken.Location)
            {
                VariableName = varName,
                Expr = expr
            };
        }











        public Function ParseExpression(List<Error> errors)
        {
            return null;
        }











        private bool VariableNameValidator(string name)
        {
            if(name==null)
            {
                return false;
            }

            if (Char.IsDigit(name[0]))
            {
                return false;
            }

            foreach(char c in name)
            {
                if(!char.IsLetterOrDigit(c) &&  c != '_')
                {
                    return false;
                }
            }
            return true;

        }











        private bool GoToCheck(List<Error> errors)
        {
            Token token = Stream.Advance();

            if(token.Value != TokenValue.OpenBracket)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, $"Syntax Error. '(' expected", Stream.Advance().Location));
                return false;
            }

            return true;
            
        }

        private GoTo ParseGoto(List<Error> errors)
        {
            Token gotoToken = Stream.Advance(); // debería ser 'goto'
            if (gotoToken.Value != TokenValue.GoTo)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "'goto' expected", gotoToken.Location));
                return null;
            }

            Token openParen = Stream.Advance();
            if (openParen.Value != TokenValue.OpenBracket)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "'(' expected after 'goto'", openParen.Location));
                return null;
            }

            Token labelToken = Stream.Advance();
            if (labelToken.Type != TokenType.Identifier)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "Label name expected", labelToken.Location));
                return null;
            }

            Token closeParen = Stream.Advance();
            if (closeParen.Value != TokenValue.CloseBracket)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "')' expected after label", closeParen.Location));
                return null;
            }

            return new GoTo(labelToken.Value,gotoToken.Location)
            {
                Label = labelToken.Value.ToString()
            };
        }


    }

}
