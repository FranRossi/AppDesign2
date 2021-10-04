using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.CustomExceptions
{
    public class InexistentBugException : Exception
    {
        public override string Message => "The bug to update does not exist on database, please enter a different bug";
    }
}