using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.Lexical;
using pixel_walle.src.CodeLocation_;

namespace pixel_walle.src.Errors
{
    public class Error : Exception
    {
        public CodeLocation Location { get; set; }


        public ErrorType Type { get; protected set; }



        public Error(ErrorType type, string message, CodeLocation location)
         : base(message)
        {
            Type = type;
            Location = location;
        }




        public override string ToString()
        {
            return Message;
        }



        public enum ErrorType
        {
            SyntaxError,                   
            TypeError,         
            SemanticError,
            Undefined,
            OutOfRange,
            ParameterTypeMismatch,
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
            GoToLabelError,                  
            EmptyStatementError,             
            DuplicateDeclarationError,      
            AssignmentError,                 
        }




    }
}
