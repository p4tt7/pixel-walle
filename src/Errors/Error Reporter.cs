using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pixel_walle.src.Errors
{
    internal class Error_Reporter
    {
        public Exception? Exception { get; private set; }

        public virtual string? Message { get; private set; }


    }
}
