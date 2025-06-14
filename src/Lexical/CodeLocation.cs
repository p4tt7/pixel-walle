namespace pixel_walle.src.CodeLocation_
{

    public class CodeLocation
    {
            public string File;
            public int Line;
            public int Column;

        public override string ToString()
        {
            return $"{File}, línea {Line}, col {Column}";
        }


    }




}
