using pixel_walle.src.AST.Expressions;
using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Instructions.Functions
{
    public class BuiltInFunction : Instruction
    {

        public string FunctionName { get; }
        public List<Expression> Parameters { get; }

        protected BuiltInFunction(string name, List<Expression> parameters, CodeLocation location) : base(location)
        {
            FunctionName = name;
            Parameters = parameters;

        }


        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            bool isInstruction = InstructionInfo.BuiltIns.ContainsKey(FunctionName);
            bool isFunction = FunctionInfo.Functions.ContainsKey(FunctionName);

            if (!isInstruction && !isFunction)
            {
                errors.Add(new Error(Error.ErrorType.SyntaxError, "", Location));
                return false;
            }

            var RealParemeter = OperationRegistry.GetParameterTypes(FunctionName);


            for (int i = 0; i < Parameters.Count && i < RealParemeter.Count; i++)
            {
                if (Parameters[i].Type != RealParemeter[i])
                {
                    errors.Add(new Error(Error.ErrorType.ParameterTypeMismatch, "", Location));
                }

            }

            return true;

        }

        public override object? Evaluate(Scope scope)
        {
            throw new NotImplementedException();
        }
    }
}
