using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pixel_walle.src.Colores;
using System.Threading.Tasks;
using pixel_walle.src.Canvas;
using System.Drawing;

namespace pixel_walle.src.AST.Instructions.Functions
{
    public class Function_Library
    {
        public void Spawn(int x, int y)
        {
            Robot robot = new Robot(x, y);

        }

        public void Color(string color)
        {
            Brush brush = new Brush();

            foreach(var c in Colors.colores)
            {
                if(Colors.colores.ContainsKey(color))
                {
                    brush.ColorBrush = Colors.colores[color];

                }

            }    
        }

        public void Size(int k)
        {

        }

        public void DrawLine(int dirX, int dirY, int distance)
        {

        }

        public void DrawCircle(int dirX, int dirY, int radius)
        {

        }

        public void DrawRectangle(int dirX, int dirY, int distance, int width, int height)
        {

        }

        public void Fill()
        {

        }

        public int GetActualX()
        {
            return 0;

        }

        public int GetActualY()
        {
            return 0;
        }

        public (int x, int y) GetCanvasSize()
        {
            return (0, 0);
        }

        public int GetColorCount(string color, int x1, int y1, int x2, int y2)
        {
            return 0;
        }

        public int IsBrushSize(int size)
        {
            return 0;
        }

        public int IsCanvasColor(string color, int vertical, int horizontal)
        {
            return 0;
        }



    }
}
