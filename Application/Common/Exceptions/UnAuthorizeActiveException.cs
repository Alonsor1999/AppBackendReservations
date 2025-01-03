using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    internal class UnAuthorizeActiveException : Exception
    {
        public UnAuthorizeActiveException()
            : base("El usuario se encuentra inactivo")
        {

        }
    }
}
