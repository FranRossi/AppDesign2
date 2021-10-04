using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainUtilities.CustomExceptions
{
    public class UserPropertyIsNullException : DomainValidationException
    {
        public UserPropertyIsNullException()
        {
            this.messageToDisplay = "User has a value that it is null";
        }
    }
}
