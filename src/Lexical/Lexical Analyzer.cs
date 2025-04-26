using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.Lexical
{
    public class Compiling
    {
        private static LexicalAnalizer? _LexicalProcess;
        public static LexicalAnalizer Lexical
        {
            get
            {
                if(_LexicalProcess==null)
                {
                    _LexicalProcess = new LexicalAnalizer();

                    _LexicalProcess.RegisterOperator("+", TokenValues.Add);
                    _LexicalProcess.RegisterOperator("-", TokenValues.Sub);
                    _LexicalProcess.RegisterOperator("*", TokenValues.Mul);
                    _LexicalProcess.RegisterOperator("/", TokenValues.Div);
                    _LexicalProcess.RegisterOperator("**", TokenValues.Pow);
                    _LexicalProcess.RegisterOperator("%", TokenValues.Mod);

                    _LexicalProcess.RegisterOperator("←", TokenValues.Assign);

                    _LexicalProcess.RegisterOperator("&&", TokenValues.And);
                    _LexicalProcess.RegisterOperator("||", TokenValues.Or);
                    _LexicalProcess.RegisterOperator("<", TokenValues.LessThan);
                    _LexicalProcess.RegisterOperator("<=", TokenValues.LessOrEqualThan);
                    _LexicalProcess.RegisterOperator(">", TokenValues.GreaterThan);
                    _LexicalProcess.RegisterOperator(">=", TokenValues.GreaterOrEqualThan);

                    _LexicalProcess.RegisterOperator(",", TokenValues.Comma);
                    _LexicalProcess.RegisterOperator("(", TokenValues.OpenBracket);
                    _LexicalProcess.RegisterOperator(")", TokenValues.CloseBracket);
                    _LexicalProcess.RegisterOperator("{", TokenValues.OpenCurlyBraces);
                    _LexicalProcess.RegisterOperator("}", TokenValues.ClosedCurlyBraces);

                    _LexicalProcess.RegisterKeyword("GoTo", TokenValues.GoTo);
                    _LexicalProcess.RegisterKeyword("Spawn", TokenValues.Spawn);
                    _LexicalProcess.RegisterKeyword("Color", TokenValues.Color);
                    _LexicalProcess.RegisterKeyword("Size", TokenValues.Size);
                    _LexicalProcess.RegisterKeyword("DrawLine", TokenValues.DrawLine);
                    _LexicalProcess.RegisterKeyword("DrawCircle", TokenValues.DrawCircle);
                    _LexicalProcess.RegisterKeyword("DrawRectangle", TokenValues.DrawRectangle);
                    _LexicalProcess.RegisterKeyword("Fill", TokenValues.Fill);

                    _LexicalProcess.RegisterKeyword("GetActualX", TokenValues.GetActualX);
                    _LexicalProcess.RegisterKeyword("GetActualY", TokenValues.GetActualY);
                    _LexicalProcess.RegisterKeyword("GetCanvasSize", TokenValues.GetCanvasSize);
                    _LexicalProcess.RegisterKeyword("GetColorCount", TokenValues.GetColorCount);
                    _LexicalProcess.RegisterKeyword("IsBrushColor", TokenValues.IsBrushColor);
                    _LexicalProcess.RegisterKeyword("IsBrushSize", TokenValues.IsBrushSize);
                    _LexicalProcess.RegisterKeyword("IsCanvasColor", TokenValues.IsCanvasColor);



                }
                return _LexicalProcess;
            }
        }
    }
}
