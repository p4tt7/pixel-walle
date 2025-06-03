using pixel_walle.src.Canvas;
using pixel_walle.src.Colores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace pixel_walle.src
{
    public class Context
    {
        public Robot? Robot { get; private set; }
        public Brush Brush { get; private set; }
        public Scope CurrentScope { get; set; }
        public Canva canvas { get; private set; }
        public Scope Scope { get; }

        public Context(Scope scope)
        {
            Scope = scope;
            CurrentScope = new Scope();
            Brush = new Brush(Colors.colores["White"], 0);
        }


        public void Spawn(int x, int y)
        {
            Robot = new Robot(x, y);

        }

        public bool HasRobot => Robot != null;
    }

}
