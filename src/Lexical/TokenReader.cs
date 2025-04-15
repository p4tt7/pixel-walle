using pixel_walle.src.Errors;

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

        public bool EOF => position >= code.Length;

        public (string file, int line, int column) Location
        {
            get { return (fileName, line, position-lastLineBreakPos); }
        }

        public char Read()
        {
            if (EOF)
            {
                throw new InvalidOperationException("EOF reached");
            }

            char current = code[position++];

            if (current == '\n')
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

            if (start && !char.IsLetter(c))
                throw new SyntaxError($"Carácter inicial inválido: '{c}'", fileName, line, position - lastLineBreakPos, code);

            if (!start && !char.IsLetterOrDigit(c))
                throw new SyntaxError($"Carácter inicial inválido: '{c}'", fileName, line, position - lastLineBreakPos, code);

            return true;
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
            while (!EOF && char.IsWhiteSpace(Peek()))
                Read();
        }


        public bool ReadID(out string id)
        {
            id = "";
            while (!EOF && ValidIDCharacter(Peek(), id.Length == 0))
                id += Read();
            return id.Length > 0;
        }

        public bool ReadNumber(out string number)
        {
            number = "";

            if(!EOF && Peek() == '-')
            {
                throw new InvalidOperationException("Negative numbers aren't allowed");
            }

            while (!EOF && char.IsDigit(Peek()))
                number += Read();

            return number.Length > 0;
        }

       


    }
}
