using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
                    : base($"La propiedad {name} : Con el valor  '({key})' no se encontró")
        {
        }
        public NotFoundException(string name)
            : base(name)
        {
        }
    }
}
