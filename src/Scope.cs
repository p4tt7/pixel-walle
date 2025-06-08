using System;
using pixel_walle.src.AST.Expressions.Atomic;
using System.Collections.Generic;
using System.Linq;

using pixel_walle.src.AST.Instructions;
using System.Text;
using pixel_walle.src.AST.Expressions;
using System.Threading.Tasks;

namespace pixel_walle.src
{
    public class Scope
    {

        private Dictionary<string, object> variables = new();
        private Dictionary<string, ExpressionType> types = new();


        public static Scope Current { get; private set; }
        public Dictionary<string, Label> Labels { get; } = new Dictionary<string, Label>();


        public Scope(Scope current = null)
        {
            variables = new Dictionary<string, object>();
            Current = current; 
        }



        public void Define(string name, object? value, ExpressionType type)
        {
            variables[name] = value;
            types[name] = type;
        }





        public object? GetVariable(string name)
        {
            if (variables.ContainsKey(name))
            {
                return variables[name];
            }

            throw new Exception($"Variable '{name}' no definida.");
        }

        public bool Exists(string name)
        {
            return variables.ContainsKey(name);
        }

        public ExpressionType GetType(string name)
        {
            return types[name];
        }








    }

}
