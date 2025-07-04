﻿using pixel_walle.src.AST.Expressions;
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
                while (Stream.Peek().Type == TokenType.Newline)
                {
                    Stream.Advance();
                }

                if (Stream.Peek().Type == TokenType.EOF)
                    break;

                Instruction? instr = ParseInstruction(errors);
                if (instr != null)
                {
                
                    instructions.Add(instr);
                }
                else
                {
                    SkipUntilNewline();
                }

            }

            return new PixelWalleProgram(
                instructions,
                instructions.Count > 0 ? instructions[0].Location : new CodeLocation { File = "program", Line = 0, Column = 0 }
            );
        }


        public Instruction ParseInstruction(List<Error> errors)
        {
            Token token = Stream.Peek();

            if (token.Type == TokenType.Newline)
            {
                Stream.Advance();
                return null;
            }

            Instruction? instr = null;

            if (token.Value == TokenValue.GoTo)
            {
                instr = ParseGoTo(errors);
            }
            else if (token.Type == TokenType.Identifier)
            {
                Token next = Stream.Peek(1);

                if (next.Type == TokenType.Symbol && next.Value == TokenValue.OpenRoundBracket)
                {
                    Token identifierToken = Stream.Advance();
                    FunctionExpression? functionExpr = ParseFunctionExpression(errors, identifierToken);

                    if (functionExpr != null)
                    {
                        instr = new FunctionInstruction(functionExpr.FunctionName, functionExpr.Arguments, identifierToken.Location);
                    }
                }
                else if (next.Type == TokenType.Symbol && next.Value == TokenValue.Assign)
                {
                    instr = ParseAssignment(errors);
                }
                else
                {
                    instr = ParseLabel(errors);
                }
            }

            if (instr != null)
            {
                Token next = Stream.Peek();
                if (next.Type != TokenType.Newline && next.Type != TokenType.EOF)
                {
                    errors.Add(new Error(Error.ErrorType.SyntaxError,
                        "Expected newline after instruction",
                        next.Location));
                    SkipUntilNewline();
                }
                else if (next.Type == TokenType.Newline)
                {
                    Stream.Advance();
                }
            }

            return instr;
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
            Expression operationLeft = ParseComparison(errors);  //se asocia a la izquierda del proximo nivel
            if (operationLeft == null) return null;

            while (Stream.Peek().Value == TokenValue.And)
            {
                Token op = Stream.Advance();
                Expression right = ParseComparison(errors); // parte derecha, proximo nivel
                if (right == null) return operationLeft;

                operationLeft = new And(operationLeft, right, op.Location);
            }

            return operationLeft;
        }




        private Expression ParseComparison(List<Error> errors)
        {
            Expression leftOperation = ParseAdditive(errors); 
            if (leftOperation == null) return null;

            while (true)
            {
                Token op = Stream.Peek();
                Expression right;

                switch (op.Value)
                {
                    case TokenValue.LessThan:
                        Stream.Advance();
                        right = ParseAdditive(errors);
                        if (right == null) return leftOperation;
                        leftOperation = new LessThan(leftOperation, right, op.Location);
                        break;
                    case TokenValue.GreaterThan:
                        Stream.Advance();
                        right = ParseAdditive(errors);
                        if (right == null) return leftOperation;
                        leftOperation = new GreaterThan(leftOperation, right, op.Location);
                        break;
                    case TokenValue.Equal:
                        Stream.Advance();
                        right = ParseAdditive(errors);
                        if (right == null) return leftOperation;
                        leftOperation = new Equal(leftOperation, right, op.Location);
                        break;
                    case TokenValue.LessOrEqualThan:
                        Stream.Advance();
                        right = ParseAdditive(errors);
                        if (right == null) return leftOperation;
                        leftOperation = new LessThanOrEqual(leftOperation, right, op.Location);
                        break;
                    case TokenValue.GreaterOrEqualThan:
                        Stream.Advance();
                        right = ParseAdditive(errors);
                        if (right == null) return leftOperation;
                        leftOperation = new GreaterThanOrEqual(leftOperation, right, op.Location);
                        break;
                    default:
                        return leftOperation;
                }
            }
        }

        private Expression ParseAdditive(List<Error> errors)
        {
            Expression leftOperation = ParseTerm(errors);
            if (leftOperation == null) return null;
            while (true)
            {
                Token op = Stream.Peek();
                if (op.Type == TokenType.EOF ||
                   (op.Value != TokenValue.Add && op.Value != TokenValue.Sub))
                    break;

                Stream.Advance();
                Expression right = ParseTerm(errors);

                leftOperation = op.Value == TokenValue.Add
                    ? new Add(leftOperation, right, op.Location)
                    : new Sub(leftOperation, right, op.Location);
            }

            return leftOperation;
        }


        public Expression ParseTerm(List<Error> errors)
        {
            Expression leftOperation = ParsePower(errors);
            if (leftOperation == null) return null;

            while (true)
            {
                Token op = Stream.Peek();

                if (op.Type == TokenType.EOF ||
                   (op.Value != TokenValue.Mul && op.Value != TokenValue.Div && op.Value != TokenValue.Mod))
                    break;

                Stream.Advance();
                Expression right = ParsePower(errors);
                if (right == null) return leftOperation;

                switch (op.Value)
                {
                    case TokenValue.Mul:
                        leftOperation = new Multiplication(leftOperation, right, op.Location);
                        break;
                    case TokenValue.Div:
                        leftOperation = new Div(leftOperation, right, op.Location);
                        break;
                    case TokenValue.Mod:
                        leftOperation = new Mod(leftOperation, right, op.Location);
                        break;
                }
            }

            return leftOperation;
        }



        public Expression ParsePower(List<Error> errors)
        {
            Expression baseExpr = ParsePrimary(errors); // asociatividad a la derecha en las potencias, (5**2)**3) 
            if (baseExpr == null) return null;

            Token op = Stream.Peek();
            if (op.Value == TokenValue.Pow)
            {
                Stream.Advance();
                Expression exponent = ParsePower(errors);
                if (exponent == null)
                {
                    errors.Add(new Error(Error.ErrorType.SyntaxError, "Expected expression after '**'", op.Location));
                    return baseExpr;
                }
                return new Pow(baseExpr, exponent, op.Location);
            }

            return baseExpr;
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

                if (Stream.Peek().Value == TokenValue.OpenRoundBracket)
                {
                    return ParseFunctionExpression(errors, identifierToken);
                }

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
                    Stream.Advance();
                    return null;
            }
        }






        public FunctionExpression ParseFunctionExpression(List<Error> errors, Token identifierToken)
        {
            string functionName = identifierToken.Value;

            if (!Stream.Match(TokenValue.OpenRoundBracket))
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "Expected '(' after function name", Stream.Peek().Location));
                return null;
            }

            List<Expression> arguments = new();

            if (Stream.Peek().Value != TokenValue.CloseRoundBracket)
            {
                while (true)
                {
                    Expression arg = ParseExpression(errors);
                    if (arg == null)
                    {
                        errors.Add(new Error(Error.ErrorType.SyntaxError, "Argument expected", Stream.Peek().Location));
                        return null;
                    }

                    arguments.Add(arg);

                    Token next = Stream.Peek();

                    if (next.Value == TokenValue.Comma)
                    {
                        Stream.Advance();
                        continue;
                    }
                    else if (next.Value == TokenValue.CloseRoundBracket)
                    {
                        break;
                    }
                    else
                    {
                        errors.Add(new Error(Error.ErrorType.SyntaxError, $"Expected ',' or ')' between arguments", next.Location));

                        while (Stream.Peek().Type != TokenType.EOF && Stream.Peek().Value != TokenValue.CloseRoundBracket)
                        {
                            Stream.Advance();
                        }

                        break;
                    }
                }
            }


            Token closingToken = Stream.Advance();

            if (closingToken.Type == TokenType.EOF || closingToken.Value != TokenValue.CloseRoundBracket)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "Expected ')' after function arguments", closingToken.Location));
                return null;
            }

            return new FunctionExpression(functionName, arguments, identifierToken.Location);
        }









        private Assignment ParseAssignment(List<Error> errors)
        {
            Token nameToken = Stream.Advance();
            string varName = nameToken.Value.ToString();

            if (Stream.Peek().Value != TokenValue.Assign)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError,
                    "'<-' expected",
                    Stream.Peek().Location));
                return null;
            }
            Token assignToken = Stream.Advance();

            Expression expr = ParseExpression(errors);
            if (expr == null)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError,
                    "Expression expected after '<-'",
                    assignToken.Location));
                return null;
            }

            Token endToken = Stream.Peek();
            if (endToken.Type != TokenType.Newline && endToken.Type != TokenType.EOF)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError,
                    "New line required after assignment",
                    endToken.Location));

                while (!Stream.End && Stream.Peek().Type != TokenType.Newline)
                {
                    Stream.Advance();
                }
            }

            return new Assignment(varName, expr, nameToken.Location);
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

        void SkipUntilNewline()
        {
            while (!Stream.End && Stream.Peek().Type != TokenType.Newline)
                Stream.Advance();
            if (!Stream.End)
                Stream.Advance();
        }



    }
}