using pixel_walle.src.AST.Instructions;
using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST
{
    public class PixelWalleProgram : ASTNode
    {
        public List<Instruction> Instructions { get; }
        private int currentIndex = 0;
        private Dictionary<string, int> LabelPositions = new();

        public PixelWalleProgram(List<Instruction> instructions, CodeLocation location) : base(location)
        {
            Instructions = instructions;

            for (int i = 0; i < Instructions.Count; i++)
            {
                if (Instructions[i] is Label label)
                    LabelPositions[label.Name] = i;
            }
        }

        public bool IsFinished => currentIndex >= Instructions.Count;

        public bool ExecuteNextInstruction(Context context, List<Error> errors)
        {

            if (currentIndex >= Instructions.Count)
                return false;

            Instruction current = Instructions[currentIndex];
            object? result = current.Evaluate(context, errors);

            if (errors.Count > 0)
                return false;

            if (current is GoTo && result is string labelName)
            {
                if (LabelPositions.TryGetValue(labelName, out int jumpTo))
                {
                    currentIndex = jumpTo;
                    return true;
                }
                else
                {
                    errors.Add(new Error(Error.ErrorType.Undefined, $"Label '{labelName}' is not defined in the current scope.", current.Location));
                    return false;
                }
            }

            currentIndex++;
            return true;
        }

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            if (Instructions.Count == 0)
            {
                return false;
            }

            if (Instructions[0] is not FunctionInstruction spawnInstruction || spawnInstruction.FunctionName != "Spawn") //arreglar
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, "The first instruction must be a Spawn(x, y)", Instructions[0].Location));
                return false;
            }

            foreach (var instruction in Instructions)
            {
                if(!instruction.CheckSemantic(scope, errors))
                {
                    return false;
                }
            }

            

            return true;
        }
    }

}
