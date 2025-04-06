using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.models.Type.Bool
{
    class LogicOperators
    {
        public bool And(bool a, bool b) {  return a && b; }
        public bool Or(bool a, bool b) {return a || b; }
        public bool Equals(bool a, bool b) { if (a == b) return true; return false; }
    }
}
