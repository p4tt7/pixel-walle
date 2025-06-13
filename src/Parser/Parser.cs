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
using System.IO;
using pixel_walle.src.CodeLocation_;
using pixel_walle.src.AST.Expressions.Binary.Binary_Operations;
using pixel_walle.src.AST.Expressions.Binary;


namespace pixel_walle.src.Parser
{
    public class Parser
    {

        public TokenStream Stream { get; private set; } 



        public Parser(TokenStream stream)
        {
            Stream = stream;
        }

        public PixelWalleProgram Parse(List<Error> errors)
        {
            List<Instruction> instructions = new();

            while (Stream.Peek().Type != TokenType.EOF)
            {
                Instruction? instr = ParseInstruction(errors);

                if (instr != null)
                {
                    instructions.Add(instr);
                }
            }

       
            return new PixelWalleProgram(instructions, instructions.Count > 0 ? instructions[0].Location : new CodeLocation { File = "program", Line = 0, Column = 0 });

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

                if (next.Type == TokenType.Symbol && next.Value == TokenValue.Assign)
                {
                    return ParseAssignment(errors);
                }

                return ParseLabel(errors);

            }



            errors.Add(new Error(Error.ErrorType.SyntaxError, $"Expected an instruction (assignment, function call, or 'GoTo'), but found '{token.Value}'", token.Location));

            while (true)
            {
                Token next = Stream.Peek();

                if (next.Type == TokenType.EOF ||
                    next.Type == TokenType.Identifier ||
                    next.Value == TokenValue.GoTo)
                {
                    break;
                }

                Stream.Advance();
            }

            return null;


        }



        public Expression? ParseNumber()
        {
            Token token = Stream.Advance();

            if (token.Type != TokenType.Number)
                return null;

            int number = int.Parse(token.Value.ToString());

            return new Number(number, token.Location);

             
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
            return ParseOr(errors);
        }




        private Expression ParseOr(List<Error> errors)
        {
            Expression left = ParseAnd(errors);
            if (left == null) return null;

            while (Stream.Peek().Value == TokenValue.Or)
            {
                Token op = Stream.Advance();
                Expression right = ParseAnd(errors);
                if (right == null) return left;

                left = new Or(left, right, op.Location);
            }

            return left;
        }




        private Expression ParseAnd(List<Error> errors)
        {
            Expression left = ParseComparison(errors);
            if (left == null) return null;

            while (Stream.Peek().Value == TokenValue.And)
            {
                Token op = Stream.Advance();
                Expression right = ParseComparison(errors);
                if (right == null) return left;

                left = new And(left, right, op.Location);
            }

            return left;
        }




        private Expression ParseComparison(List<Error> errors)
        {
            Expression left = ParseAdditive(errors);
            if (left == null) return null;

            while (true)
            {
                Token op = Stream.Peek();
                Expression right;

                switch (op.Value)
                {
                    case TokenValue.LessThan:
                        Stream.Advance();
                        right = ParseAdditive(errors);
                        if (right == null) return left;
                        left = new LessThan(left, right, op.Location);
                        break;
                    case TokenValue.GreaterThan:
                        Stream.Advance();
                        right = ParseAdditive(errors);
                        if (right == null) return left;
                        left = new GreaterThan(left, right, op.Location);
                        break;
                    case TokenValue.Equal:
                        Stream.Advance();
                        right = ParseAdditive(errors);
                        if (right == null) return left;
                        left = new Equal(left, right, op.Location);
                        break;
                    case TokenValue.LessOrEqualThan:
                        Stream.Advance();
                        right = ParseAdditive(errors);
                        if (right == null) return left;
                        left = new LessThanOrEqual(left, right, op.Location);
                        break;
                    case TokenValue.GreaterOrEqualThan:
                        Stream.Advance();
                        right = ParseAdditive(errors);
                        if (right == null) return left;
                        left = new GreaterThanOrEqual(left, right, op.Location);
                        break;
                    default:
                        return left;
                }
            }
        }

        private Expression ParseAdditive(List<Error> errors)
        {
            Expression left = ParseTerm(errors);
            if (left == null) return null;

            while (true)
            {
                Token op = Stream.Peek();
                if (op.Type == TokenType.EOF ||
                   (op.Value != TokenValue.Add && op.Value != TokenValue.Sub))
                    break;

                Stream.Advance();
                Expression right = ParseTerm(errors);

                left = op.Value == TokenValue.Add
                    ? new Add(left, right, op.Location)
                    : new Sub(left, right, op.Location);
            }

            return left;
        }


        public Expression ParseTerm(List<Error> errors)
        {
            Expression left = ParsePower(errors);
            if (left == null) return null;

            while (true)
            {
                Token op = Stream.Peek();

                if (op.Type == TokenType.EOF ||
                   (op.Value != TokenValue.Mul && op.Value != TokenValue.Div && op.Value != TokenValue.Mod))
                    break;

                Stream.Advance();
                Expression right = ParsePower(errors);
                if (right == null) return left;

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
            if (left == null) return null;

            Token op = Stream.Peek();
            if (op.Value == TokenValue.Pow)
            {
                Stream.Advance();
                Expression right = ParsePower(errors);
                if (right == null)
                {
                    errors.Add(new Error(Error.ErrorType.SyntaxError, "Expected expression after '**'", op.Location));
                    return left;
                }
                return new Pow(left, right, op.Location);
            }

            return left;
        }



        public Expression ParsePrimary(List<Error> errors)
        {
            Token token = Stream.Peek();

            if (token.Type == TokenType.EOF)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "Unexpected end of expression", token.Location));
                return null;
            }

            if (token.Value == TokenValue.Sub)
            {
                Stream.Advance();
                Expression operand = ParsePrimary(errors);

                if (operand == null)
                {
                    errors.Add(new Error(Error.ErrorType.SyntaxError, "Expected expression after unary '-'", token.Location));
                    return null;
                }

                return new Negative(operand, token.Location);
            }

            if (token.Value == TokenValue.OpenRoundBracket)
            {
                Stream.Advance();
                Expression expr = ParseExpression(errors);

                if (Stream.Peek().Value != TokenValue.CloseRoundBracket)
                {
                    errors.Add(new Error(Error.ErrorType.SyntaxError, "Expected ')'", Stream.Peek().Location));
                }
                else
                {
                    Stream.Advance();
                }

                return expr;
            }

            if (token.Type == TokenType.Identifier)
            {
                Token identifierToken = Stream.Advance();
                return new Variable(identifierToken.Value, identifierToken.Location);
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

            if (token.Type == TokenType.EOF)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "Unexpected end of input in assignment", token.Location));
                return null;
            }

            if (!Assignment.VariableNameValidator(token.Value.ToString()))
            {
                errors.Add(new Error(Error.ErrorType.AssignmentError, $"Invalid variable name", Stream.Advance().Location));
            }

            Token assignToken = Stream.Advance();

            if (assignToken.Type == TokenType.EOF || assignToken.Value != TokenValue.Assign)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "'<-' expected", assignToken.Location));
                return null;
            }

            return ParseExpression(errors);

        }


        public Function ParseFunctionCall(List<Error> errors, Token identifierToken)
        {
            string functionName = identifierToken.Value;

            if (!FunctionLibrary.BuiltIns.TryGetValue(functionName, out FunctionInfo info))
            {
                errors.Add(new Error(
                    Error.ErrorType.SemanticError,
                    $"Function '{functionName}' is not defined",
                    identifierToken.Location
                ));
                return null; 
            }

            if (Stream.Peek().Type == TokenType.EOF)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "Unexpected end of input after function name", identifierToken.Location));
                return null;
            }


            if (!Stream.Match(TokenValue.OpenRoundBracket))
            {
                errors.Add(new Error(
                    Error.ErrorType.SyntaxError,
                    $"Expected '{TokenValue.OpenRoundBracket}' after function name",
                    Stream.Advance().Location
                ));
                return null;
            }

            List<Expression> arguments = new List<Expression>();

            if (Stream.Peek().Value != TokenValue.CloseRoundBracket)
            {
                do
                {
                    Expression arg = ParseExpression(errors);
                    if (arg == null)
                    {
                        errors.Add(new Error(Error.ErrorType.SyntaxError, "Argument expected", Stream.Peek().Location));
                        return null;
                    }

                    arguments.Add(arg);
                } while (Stream.Match(TokenValue.Comma));
            }

            Token closingToken = Stream.Advance();

            if (closingToken.Type == TokenType.EOF || closingToken.Value != TokenValue.CloseRoundBracket)
            {
                errors.Add(new Error(
                    Error.ErrorType.SyntaxError,
                    $"Expected '{TokenValue.CloseRoundBracket}' after function arguments",
                    closingToken.Location
                ));
                return null;
            }


            if (closingToken.Value != TokenValue.CloseRoundBracket)
            {
                errors.Add(new Error(
                    Error.ErrorType.SyntaxError,
                    $"Expected '{TokenValue.CloseRoundBracket}' after function arguments",
                    closingToken.Location
                ));
                return null;
            }

            if (arguments.Count != info.Parameters.Count)
            {
                errors.Add(new Error(
                    Error.ErrorType.SemanticError,
                    $"Function '{functionName}' expects {info.Parameters.Count} arguments, but {arguments.Count} were provided",
                    identifierToken.Location
                ));
                return null;
            }

            return new Function(
                functionName,
                arguments,
                identifierToken.Location
            );
        }




        private Assignment ParseAssignment(List<Error> errors)
        {
            Token nameToken = Stream.Advance(); 

            string varName = nameToken.Value.ToString();

            Token assignToken = Stream.Advance();

            if (assignToken.Type == TokenType.EOF || assignToken.Value != TokenValue.Assign)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "'<-' expected", assignToken.Location));
                return null;
            }

            if (assignToken.Value != TokenValue.Assign)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "'<-' expected", assignToken.Location));
            }

            Expression expr = ParseExpression(errors);

            if (expr == null)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "Expression expected after '<-'", assignToken.Location));
                return null;
            }

            return new Assignment(varName, expr, assignToken.Location);

        }




        private GoTo? ParseGoTo(List<Error> errors)
        {
            Token goToToken = Stream.Advance();

 
            Token openBracket = Stream.Peek();
            if (openBracket.Value != TokenValue.OpenSquareBracket)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "'[' expected", openBracket.Location));
                return null;
            }
            Stream.Advance();


            Token labelToken = Stream.Peek();
            if (labelToken.Type != TokenType.Identifier)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "Label name expected", labelToken.Location));
                return null;
            }
            string labelName = labelToken.Value;
            Stream.Advance();


            Token closeBracket = Stream.Peek();
            if (closeBracket.Value != TokenValue.ClosedSquareBracket)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "']' expected", closeBracket.Location));
                return null;
            }
            Stream.Advance();


            Token openParen = Stream.Peek();
            if (openParen.Value != TokenValue.OpenRoundBracket)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "'(' expected", openParen.Location));
                return null;
            }
            Stream.Advance();


            Expression? condition = ParseExpression(errors);
            if (condition == null)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "Condition expected inside parentheses", openParen.Location));
                return null;
            }


            Token closeParen = Stream.Peek();
            if (closeParen.Value != TokenValue.CloseRoundBracket)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "')' expected", closeParen.Location));
                return null;
            }
            Stream.Advance();

            return new GoTo(labelName, condition, goToToken.Location);
        }







        private Label? ParseLabel(List<Error> errors)
        {
            Token labelToken = Stream.Advance();


            if (labelToken.Type != TokenType.Identifier)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "Expected label name (identifier)", labelToken.Location));
                return null;
            }

            return new Label(labelToken.Value, labelToken.Location);
        }



    }

}
