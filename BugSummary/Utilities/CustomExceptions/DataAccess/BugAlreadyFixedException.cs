using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.CustomExceptions;

namespace CustomExceptions
{
    public class BugAlreadyFixedException : DataAccessException
    {
        public BugAlreadyFixedException()
        {
            this.messageToDisplay = "The bug you are trying to fix is already fixed.";
        }
    }
}