using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.CustomExceptions
{
    public class InexistentUserException : Exception
    {
        public override string Message => "The entered user does not exist.";
    }
}
