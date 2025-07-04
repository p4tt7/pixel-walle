﻿using pixel_walle.src.Errors;
using pixel_walle.src.CodeLocation_;

namespace pixel_walle.src.Lexical
{
    public class TokenReader
    {
        private readonly string fileName;
        private readonly string code;
        private int position;
        private int line;
        private int lastLineBreakPos;

        public TokenReader(string fileName, string code)
        {
            this.fileName = fileName;
            this.code = code;
            this.position = 0;
            this.line = 1;
            this.lastLineBreakPos = -1;

        }

        public CodeLocation Location
        {
            get
            {
                return new CodeLocation
                {
                    File = fileName,
                    Line = line,
                    Column = position - lastLineBreakPos
                };
            }
        }

        public bool EOF => position >= code.Length;
        public bool EOL => EOF || code[position] == '\n';


        public char Read()
        {
            if (EOF)
            {
                throw new InvalidOperationException("EOF reached");
            }

            char current = code[position++];

            if (current == '\r')
            {
                if (position < code.Length && code[position] == '\n')
                    position++; 

                line++;
                lastLineBreakPos = position;
            }
            else if (current == '\n')
            {
                line++;
                lastLineBreakPos = position;
            }

            return current;

        }


        public bool Match(string pattern)
        {
            if (position + pattern.Length > code.Length)
            {
                return false;
            }

            for (int i = 0; i < pattern.Length; i++)
            {
                if (code[position + i] != pattern[i])
                { 
                    return false; 
                }
               
            }

            position += pattern.Length;
            return true;

        }


        public bool ValidIDCharacter(char c, bool start)
        {
            if (c == '_') return true;
            if (start) return char.IsLetter(c);
            return char.IsLetterOrDigit(c);
        }



        public char Peek()
        {

            if (position < 0 || position >= code.Length)
            {
                throw new InvalidOperationException();
            }

            return code[position];

        }

        public void SkipWhitespace()
        {
            while (!EOF && (Peek() == ' ' || Peek() == '\t')) 
                Read();
        }

        public bool ReadID(out string id)
        {
            id = "";
            while (!EOF && ValidIDCharacter(Peek(), id.Length == 0))
                id += Read();
            return id.Length > 0;
        }

        public bool IsNewLine()
        {
            if (EOF) return false;

            char c = code[position];
            if (c == '\n') return true;
            if (c == '\r' && position + 1 < code.Length && code[position + 1] == '\n') return true;

            return false;
        }

        public bool ReadNumber(out string number)
        {
            number = "";

            while (!EOF && char.IsDigit(Peek()))
            {
                number += Read();
            }

            if (!EOF && (char.IsLetter(Peek()) || Peek() == '_'))
            {
                return false;
            }

                return number.Length > 0;
        }


        public bool ReadText(out string text)
        {
            text = "";

            if (Peek() != '"')
            {
                return false;
            }

            Read();

            while (!EOF)
            {
                char c = Peek();

                if (c == '"')
                {
                    Read(); 
                    return true;
                }

                if (c == '\n' || c == '\r')
                {
                    return false;
                }

                text += Read();
            }

            return false;
        }




        public bool ReadWhiteSpace()
        {
            if (char.IsWhiteSpace(Peek()))
            {
                ReadAny();
                return true;
            }
            return false;
        }

        public char ReadAny()
        {
            if (EOF)
                throw new InvalidOperationException();

            if (EOL)
            {
                line++;
                lastLineBreakPos = position;
                if (Peek() == '\r' && position + 1 < code.Length && code[position + 1] == '\n')
                    position++;
            }
            return code[position++];
        }


    }
}
