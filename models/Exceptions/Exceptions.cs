using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.models.Exceptions
{
    abstract class Exceptions
    {
        public abstract string Name { get; }

        public virtual void Description()
        {
            Console.WriteLine("An error has occured");
        }
    }
}
