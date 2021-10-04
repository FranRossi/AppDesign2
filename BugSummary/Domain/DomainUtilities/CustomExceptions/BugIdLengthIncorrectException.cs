using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainUtilities.CustomExceptions
{
    public class BugIdLengthIncorrectException : DomainValidationException
    {
        public BugIdLengthIncorrectException()
        {
            this.messageToDisplay = "Bug's id must be under 4 characters";
        }
    }
}
