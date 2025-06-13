namespace pixel_walle.src.CodeLocation_
{

    public class CodeLocation
    {
            public string File;
            public int Line;
            public int Column;

        public override string ToString()
        {
            int line = Line + 1;
            int column = Column + 1;

            return $"{File}, línea {line}, col {column}";
        }


    }




}
