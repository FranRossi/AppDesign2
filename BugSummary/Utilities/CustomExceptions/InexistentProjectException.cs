using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.CustomExceptions
{
    public class InexistentProjectException : Exception
    {
        public override string Message => "The entered project does not exist.";
    }
}
