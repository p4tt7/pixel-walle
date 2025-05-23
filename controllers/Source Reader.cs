using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.controllers
{
    public class SourceReader
    {
        public string SourceCode { get; set; }

        public void TextCode(string texto)
        {

            SourceCode = texto;

        }
    }
}
