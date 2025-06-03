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
                return ParseGoTo(errors);
            }

            if (token.Type == TokenType.Identifier)
            {
                Token next = Stream.Peek(1);

                if (next.Type == TokenType.Symbol && next.Value == TokenValue.OpenRoundBracket)
                {
                    Token identifierToken = Stream.Advance();
                    return ParseFunctionCall(errors, identifierToken);
                }

                return ParseAssignment(errors);
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

            if (token.Value == TokenValue.OpenRoundBracket)
            {
                Stream.Advance();
                Expression expr = ParseExpression(errors);

                if (!Stream.Match(TokenValue.CloseRoundBracket))
                {
                    errors.Add(new Error(Error.ErrorType.SyntaxError, "Expected ')'", Stream.Peek().Location));
                }

                return expr;
            }

            switch (token.Type)
            {
                case TokenType.Number:
                    return ParseNumber();
                case TokenType.Text:
                    return ParseText();
                case TokenType.Bool:
                    return ParseBool();
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


        public Function ParseFunctionCall(List<Error> errors, Token identifierToken)
        {

            string functionName = identifierToken.Value;


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
              

            Token token = Stream.Advance();

            if (token.Value != TokenValue.CloseRoundBracket)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError,
                    $"Expected '{TokenValue.CloseRoundBracket}'",
                    token.Location));
                return null;
            }

            FunctionInfo info = FunctionLibrary.BuiltIns[functionName];
            ExpressionType? returnType = info.ReturnType;


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







        private GoTo ParseGoTo(List<Error> errors)
        {
            Token token = Stream.Advance();
            
            if(token.Value != TokenValue.OpenSquareBracket)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "'[ expected", token.Location));
                return null;
            }

            Instruction label = null;
            Token tokenLabel = Stream.Advance();
            if (tokenLabel.Type != TokenType.Identifier)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "Label expected", token.Location));

            }
            
         
            var labelNode = new Label(tokenLabel.Value, tokenLabel.Location);

            
            Token tokenClose = Stream.Advance();
            if(tokenClose.Value != TokenValue.ClosedSquareBracket)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "']' expected", token.Location));
                return null;

            }

            Token tokenOpenP = Stream.Advance();
            if (tokenOpenP.Value != TokenValue.OpenRoundBracket)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "'(' expected", token.Location));
                return null;

            }

                  
            Expression condition = null;
            Token tokenCondition = Stream.Advance();
            condition = ParseExpression(errors);
            if(condition==null)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "Condition expected after ','", tokenCondition.Location));
            }

            Token tokenCloseP = Stream.Advance();
            if (tokenOpenP.Value != TokenValue.CloseRoundBracket)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "')' expected", token.Location));
                return null;
            }

            List<Instruction> body = new();
            if(Stream.Peek().Value == TokenValue.OpenCurlyBraces)
            {
                Stream.Advance();
                while (Stream.Peek().Value != TokenValue.ClosedCurlyBraces && !Stream.End)
                {
                    var instruction = ParseInstruction(errors);
                    if (instruction != null)
                        body.Add(instruction);
                }

                if (Stream.Peek().Value != TokenValue.ClosedCurlyBraces)
                {
                    errors.Add(new Error(Error.ErrorType.SyntaxError, "'}' expected to close GoTo block", .Location));
                    return null;
                }

                Stream.Advance();

            }

            var gotoNode = new GoTo(labelNode, condition, token.Location)
            {
                Body = body
            };

            return gotoNode;

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
