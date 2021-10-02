using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainUtilities.CustomExceptions
{
    public class BugNameLengthIncorrectException : Exception
    {
        public override string Message => "Bug's name must be under 30 characters";
    }
}
