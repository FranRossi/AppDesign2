using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainUtilities.CustomExceptions
{
    public class BugStateIncorrectException : DomainValidationException
    {
        public BugStateIncorrectException()
        {
            this.messageToDisplay = "Bug's state must be active or inactive";
        }

    }
}
