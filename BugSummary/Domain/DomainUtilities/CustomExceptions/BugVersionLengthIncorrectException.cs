using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainUtilities.CustomExceptions
{
    public class BugVersionLengthIncorrectException : DomainValidationException
    {
        public BugVersionLengthIncorrectException()
        {
            this.messageToDisplay = "Bug's version must be under 10 characters";
        }
    }
}
