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
using pixel_walle.src.AST.Expressions.Atomic;
using System.Security.RightsManagement;
using pixel_walle.src.AST.Expressions.Binary_Operations;
using pixel_walle.src.AST.Instructions.Functions;


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





        public Expression ParseBinaryOperation(List<Error> errors)
        {
            Expression left = ParseNumber();

            while(IsOperator())
            {
                Token operatorToken = Stream.Advance();

                Expression? right = ParseNumber();

                if (right == null)
                {
                    errors.Add(new Error(Error.ErrorType.SyntaxError, "Expected right-hand operand", operatorToken.Location));
                    return null;
                }

                left = BinaryConstruction(left, operatorToken, right);

            }

            return left;

        }









        public object ParseStatement(List<Error> errors)
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


            if (token.Value != TokenValue.OpenRoundBracket)
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
            //      var funcCall = new BuiltInFunction(functionName, arguments, returnType, token.Location);

            //       return funcCall;
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






        public Expression ParseExpression(List<Error> errors)
        {
            return null;
        }









        public Instruction ParseInstruction(List<Error> errors)
        {
            return null;

        }






        private static Expression BinaryConstruction(Expression left, Token operatorToken, Expression right)
        {
            switch (operatorToken.Value)
            {
                case TokenValue.Add:
                    return new Add(left, right, operatorToken.Location);

                case TokenValue.Sub:
                    return new Sub(left,right,operatorToken.Location);

                case TokenValue.Mul:
                    return new Multiplication(left,right,operatorToken.Location);

                case TokenValue.Div:
                    return new Div(left, right, operatorToken.Location);

                case TokenValue.Pow:
                    return new Pow(left, right, operatorToken.Location);

                case TokenValue.Mod:
                    return new Mod(left, right, operatorToken.Location);

                case TokenValue.GreaterThan:
                    return new GreaterThan(left,right, operatorToken.Location);

                case TokenValue.LessThan:
                    return new LessThan(left,right, operatorToken.Location);

                case TokenValue.And:
                    return new And(left, right, operatorToken.Location);

                case TokenValue.Or:
                    return new Or(left, right, operatorToken.Location);


                default:
                    return null;

            }


        }

        private bool IsOperator()
        {
            if (Token.operators.ContainsKey(Stream.Peek().Value.ToString()))
            {
                return true;
            }

            return false;
        }


    }

}
