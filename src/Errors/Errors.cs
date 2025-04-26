using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.Errors
{
    public class Error : Exception
    {
        public string FileName { get; protected set; }
        public int Line { get; protected set; }
        public int Column { get; protected set; }

        public ErrorType Type { get; protected set; }

        public Error(ErrorType type, string message, string fileName = "", int line = 0, int column = 0)
         : base($"[{type}] {message}")
        {
            Type = type;
            FileName = fileName;
            Line = line;
            Column = column;
        }


        public enum ErrorType
        {
            SyntaxError,                   
            TypeError,                       
            UndeclaredVariableError,        
            DivisionByZeroError,            
            FunctionNotFoundError,          
            ParameterCountMismatchError,  
            UnreachableCodeError,            
            InfiniteLoopError,               
            InvalidTokenError,               
            ScopeError,                     
            RedefinedVariableError,          
            BracketMismatchError,            
            UnsupportedOperationError,       
            MemoryOverflowError,            
            MissingReturnError,             
            GoToLabelError,                  
            EmptyStatementError,             
            DuplicateDeclarationError,      
            AssignmentError,                 
            KeywordError                   
        }




    }
}
