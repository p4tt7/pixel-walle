using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src
{
    public class Scope
    {

        private Dictionary<string, object> variables;

        public Scope Parent { get; private set; }

        public Scope(Scope parent = null)
        {
            variables = new Dictionary<string, object>();
            Parent = parent; 
        }


        public void SetVariable(string name, object value)
        {
            if (variables.ContainsKey(name))
            {
                variables[name] = value;
            }
            else
            {
                variables.Add(name, value);
            }
        }

        public object GetVariable(string name)
        {
            if (variables.ContainsKey(name))
            {
                return variables[name];
            }
            else if (Parent != null)
            {
                return Parent.GetVariable(name);
            }
            else
            {
                throw new Exception($"La variable '{name}' no está definida en este scope.");
            }
        }

        public bool IsVariableDefined(string name)
        {
            return variables.ContainsKey(name) || (Parent != null && Parent.IsVariableDefined(name));
        }
    }

}
