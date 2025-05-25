using pixel_walle.src.AST.Expressions;
using pixel_walle.src.Lexical;
using pixel_walle.src.AST.Instructions;
using pixel_walle.src.Errors;
using System;
using System.Linq;
using System.Text;
using pixel_walle.src.AST.Expressions.Atomic;
using System.Security.RightsManagement;
using pixel_walle.src.AST.Expressions.Binary_Operations;
using pixel_walle.src.AST.Instructions.Functions;
using pixel_walle.src.AST;


namespace pixel_walle.src.Parser
{
    public class Parser
    {

        public TokenStream Stream { get; private set; } 



        public Parser(TokenStream stream)
        {
            Stream = stream;
        }

        public ASTNode? Parse(List<Error> errors)
        {
            Token token = Stream.Peek();

            if (IsExpression(token))
            {
                return ParseExpression(errors);
            }

            if (IsInstruction(token)) 
            { 
                return ParseInstruction(errors);
            }

            return null;

        }

        public Instruction ParseInstruction(List<Error> errors)
        {
            Token token = Stream.Peek();

            if(token.Value == TokenValue.GoTo)
            {
                return ParseGoto(errors);
            }

            if (token.Type == TokenType.Identifier)
            {
                return ParseAssignment(errors);
            }

            if(OperationRegistry.IsBuiltIn(token.Value))
            {
                return ParseFunctionCall(errors);

            }



            errors.Add(new Error(Error.ErrorType.SyntaxError, "Instruction expected", token.Location));
            Stream.Advance(); 
            return null;


        }



        public Expression? ParseNumber()
        {
            Token token = Stream.Advance();

            if (token.Type != TokenType.Number)
                return null;

            int number = int.Parse(token.Value.ToString());

            return new Number(token.Location, number);

             
        }

        public Expression? ParseText()
        {
            Token token = Stream.Peek();

            if (token.Type == TokenType.Text)
            {
                Stream.Advance(); 
                return new Text(token.Location, token.Value);
            }
             
            return null;
        }



        public Expression? ParseBool()
        {
            Token token = Stream.Peek();

            if (token.Type == TokenType.Bool)
            {
                Stream.Advance();
                bool value = token.Value.ToString() == "true";
                return new Bool(token.Location, value);
            }

            return null;
        }

        public Expression ParseExpression(List<Error> errors)
        {
            Expression left = ParseTerm(errors);

            while (Stream.Match(TokenValue.Add) || Stream.Match(TokenValue.Sub))
            {
                Token op = Stream.Rollback();

                Expression right = ParseTerm(errors);

                if (op.Value == TokenValue.Add)
                    left = new Add(left, right, op.Location);
                else
                    left = new Sub(left, right, op.Location);
            }

            return left;
        }

        public Expression ParseTerm(List<Error> errors)
        {
            Expression left = ParsePower(errors);

            while (Stream.Match(TokenValue.Mul) || Stream.Match(TokenValue.Div) || Stream.Match(TokenValue.Mod))
            {
                Token op = Stream.Rollback();
                Expression right = ParsePower(errors);

                switch (op.Value)
                {
                    case TokenValue.Mul:
                        left = new Multiplication(left, right, op.Location);
                        break;
                    case TokenValue.Div:
                        left = new Div(left, right, op.Location);
                        break;
                    case TokenValue.Mod:
                        left = new Mod(left, right, op.Location);
                        break;
                }
            }

            return left;
        }

        public Expression ParsePower(List<Error> errors)
        {
            Expression left = ParsePrimary(errors);

            if (Stream.Match(TokenValue.Pow))
            {
                Token op = Stream.Rollback();
                Expression right = ParsePower(errors); 

                return new Pow(left, right, op.Location);
            }

            return left;
        }

        public Expression ParsePrimary(List<Error> errors)
        {
            Token token = Stream.Peek();

            switch (token.Type)
            {
                case TokenType.Number:
                    return ParseNumber();
                case TokenType.Text:
                    return ParseText();
                case TokenType.Bool:
                    return ParseBool();
                case TokenType.OpenRoundBracket:
                    Stream.Advance(); 
                    Expression expr = ParseExpression(errors);
                    if (!Stream.Match(TokenValue.CloseRoundBracket))
                    {
                        errors.Add(new Error(Error.ErrorType.SyntaxError, "Expected ')'", Stream.Peek().Location));
                    }
                    return expr;
                default:
                    errors.Add(new Error(Error.ErrorType.SyntaxError, "Unexpected token", token.Location));
                    Stream.Advance(); 
                    return null;
            }
        }











        public Expression ParseStatement(List<Error> errors)
        {

            Token token = Stream.Advance();

            if (!Assignment.VariableNameValidator(token.Value.ToString()))
            {
                errors.Add(new Error(Error.ErrorType.AssignmentError, $"Invalid variable name", Stream.Advance().Location));
            }

            Token assignToken = Stream.Advance();

            if (assignToken.Value != TokenValue.Assign)
            {
                errors.Add(new Error(Error.ErrorType.AssignmentError, $"Invalid assignation symbol, {TokenValue.Assign} expected", Stream.Advance().Location));

            }

            return ParseExpression(errors);

        }


        public BuiltInFunction ParseFunctionCall(List<Error> errors)
        {
            Token token = Stream.Advance();

            string functionName = token.Value.ToString();


            if (!Stream.Match(TokenValue.OpenRoundBracket))
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, $"Invalid syntax, {TokenValue.OpenRoundBracket} expected", Stream.Advance().Location));
            }

            
            List<Expression> arguments = new List<Expression>();


            if (Stream.Peek().Value != TokenValue.CloseRoundBracket)
            {
                do
                {
                    Expression arg = ParseExpression(errors); 
                    if (arg != null)
                        arguments.Add(arg);
                } while (Stream.Match(TokenValue.Comma));
            }
              

            token = Stream.Advance();

            if (token.Value != TokenValue.CloseRoundBracket)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError,
                    $"Expected '{TokenValue.CloseRoundBracket}'",
                    token.Location));
                return null;
            }

            FunctionInfo info = FunctionInfo.Functions[functionName];
            ExpressionType returnType = info.ReturnType;


            return null;

        }







        private Assignment ParseAssignment(List<Error> errors)
        {
            Token nameToken = Stream.Advance(); 

            string varName = nameToken.Value.ToString();

            Token assignToken = Stream.Advance();

            if (assignToken.Value != TokenValue.Assign)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "'<-' expected", assignToken.Location));
            }

            Expression expr = ParseExpression(errors);
            
            return new Assignment(varName, expr, assignToken.Location);

        }

        



        private GoTo ParseGoto(List<Error> errors)
        {
            Token gotoToken = Stream.Advance(); 
            if (gotoToken.Value != TokenValue.GoTo)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "'GoTo' expected", gotoToken.Location));
                return null;
            }

            Token openParen = Stream.Advance();
            if (openParen.Value != TokenValue.OpenRoundBracket)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "'(' expected after 'GoTo'", openParen.Location));
                return null;
            }

            Token labelToken = Stream.Advance();
            if (labelToken.Type != TokenType.Identifier)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "Label name expected", labelToken.Location));
                return null;
            }

            Token closeParen = Stream.Advance();
            if (closeParen.Value != TokenValue.CloseRoundBracket)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "')' expected after label", closeParen.Location));
                return null;
            }

            return new GoTo(labelToken.Value,gotoToken.Location)
            {
                Label = labelToken.Value.ToString()
            };
        }


        private bool IsExpression(Token token)
        {
            return token.Type == TokenType.Number ||
                   token.Type == TokenType.Text ||
                   token.Type == TokenType.Bool;
        }

        private bool IsInstruction(Token token)
        {
            return token.Type == TokenType.Identifier ||
                   token.Value == TokenValue.Assign || 
                   token.Value == TokenValue.GoTo ||   
                   token.Value == TokenValue.OpenRoundBracket;
        }


    }

}
