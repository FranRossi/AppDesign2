using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Exceptions
{
    public class InexistentBugException : Exception
    {
        public override string Message => "The entered bug does not exist.";
    }
}