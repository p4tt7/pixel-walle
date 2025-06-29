using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static pixel_walle.src.Lexical.Token;
using pixel_walle.src.Errors;
using System.Windows.Shapes;
using System.Windows.Media;

namespace pixel_walle.src.Lexical
{
    public class LexicalAnalizer
    {

        public bool MatchOperator(TokenReader stream, List<Token> tokens)
        {
            foreach (var op in operators.Keys.OrderByDescending(k => k.Length))
            {
                var loc = stream.Location;
                if (stream.Match(op))
                {
                    tokens.Add(new Token(TokenType.Symbol, operators[op], stream.Location));
                    return true;
                }
            }
                return false;
            
        }


        public List<Token> GetTokens(string fileName, string code, List<Error> errors)
        {
            List<Token> tokens = new List<Token>();
            TokenReader stream = new TokenReader(fileName, code);

            while (!stream.EOF)
            {
                stream.SkipWhitespace();

                if (stream.EOF) break;

                if (stream.IsNewLine())
                {
                    var loc = stream.Location;
                    char first = stream.Read();

                    if (first == '\r' && !stream.EOF && stream.Peek() == '\n')
                    {
                        stream.Read();
                    }
                    tokens.Add(new Token(TokenType.Newline, "\\n", loc));
                    continue;
                }

                string value;

                if (stream.Peek() == '\n' || stream.Peek() == '\r')
                {
                    var loc = stream.Location;
                    char newline = stream.Read();

                    if (newline == '\r' && !stream.EOF && stream.Peek() == '\n')
                        stream.Read();

                    tokens.Add(new Token(TokenType.Newline, "\\n", loc));
                    continue;
                }

                var locId = stream.Location;

                if (stream.ReadID(out value))
                {
                    TokenType type;

                    if (value == "true" || value == "false")
                    {
                        type = TokenType.Bool;
                    }
                    else if (keywords.ContainsKey(value))
                    {
                        type = TokenType.Keyword;
                    }
                    else
                    {
                        type = TokenType.Identifier;
                    }

                    tokens.Add(new Token(type, value, locId));
                    continue;
                }

                var locNum = stream.Location;

                if (stream.ReadNumber(out value))
                {
                    tokens.Add(new Token(TokenType.Number, value, locNum));
                    continue;
                }
                else if (!string.IsNullOrEmpty(value))
                {
                    errors.Add(new Error(
                        Error.ErrorType.LexicalError,
                        $"Malformed number or invalid token: '{value}'", locNum));
                    continue;
                }

                if (stream.Peek() == '"')
                {
                    var loc = stream.Location;
                    if (stream.ReadText(out value))
                    {
                        tokens.Add(new Token(TokenType.Text, value, loc));
                    }
                    else
                    {
                        errors.Add(new Error(
                            Error.ErrorType.LexicalError,
                            "Unclosed string literal", loc));

                        while (!stream.EOF && stream.Peek() != '\n' && stream.Peek() != '"')
                        {
                            stream.Read();
                        }

                        if (!stream.EOF && stream.Peek() == '"') stream.Read();
                    }

                    continue;
                }


                if (MatchOperator(stream, tokens))
                {
                    continue;
                }

                char unknownOp = stream.ReadAny();

                errors.Add(new Error(
                    Error.ErrorType.InvalidTokenError,
                    $"Unrecognized symbol: '{unknownOp}'", stream.Location));
            }

            return tokens;
        }




    }










}
