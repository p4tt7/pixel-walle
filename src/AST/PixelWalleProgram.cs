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

        public PixelWalleProgram(List<Instruction> instructions, CodeLocation location) : base(location)
        {
            Instructions = instructions;
        }

        public object? Evaluate(Context context, List<Error> errors)
        {

            Dictionary<string, int> LabelPositions = new Dictionary<string, int>();

            for (int i = 0; i < Instructions.Count; i++)
            {
                if (Instructions[i] is Label label)
                {
                    LabelPositions[label.Name] = i;
                }
            }

            int index = 0;
            int maxIterations = 10000;
            int iterations = 0;

            while (index < Instructions.Count)
            {
                if (++iterations > maxIterations)
                {
                    throw new Exception("Infinite loop detected in main program.");
                }

                Instruction instruction = Instructions[index];
                object? result = instruction.Evaluate(context, errors);

                if (instruction is GoTo && result is string labelName)
                {
                    if (LabelPositions.TryGetValue(labelName, out int newIndex))
                    {
                        index = newIndex;
                        continue;
                    }

                    else
                    {
                        throw new Exception($"Label '{labelName}' not found.");
                    }


                }

                else
                {
                    index++;
                }
            }

            return null;
        }

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            foreach(var instruction in Instructions)
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
