using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainUtilities.CustomExceptions
{
    public class BugVersionLengthIncorrectException : Exception
    {
        public override string Message => "Bug's version must be under 10 characters";
    }
}
