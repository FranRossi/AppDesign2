using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Exceptions
{
    public class BugAlreadyFixedException : Exception
    {
        public override string Message => "The bug you are trying to fix is already fixed.";
    }
}