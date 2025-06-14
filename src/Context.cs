using pixel_walle.src.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using pixel_walle.src.AST;

namespace pixel_walle.src
{
    public class Context
    {
        public Robot? Robot { get; private set; }
        public Brush Brush { get; private set; }
        public Scope CurrentScope { get; set; }
        public Canva canvas { get; private set; }
        public Scope Scope { get; }

        public Context(Scope scope, int canvasWidth, int canvasHeight)
        {
           
            Scope = scope;
            CurrentScope = new Scope();
            Brush = new Brush(ColorPalette.Colors["White"] , 0);
            canvas = new Canva(canvasWidth, canvasHeight);
        }


        public void Spawn(int x, int y)
        {
            Robot = new Robot(x, y);

        }

        public bool HasRobot => Robot != null;
    }

}
