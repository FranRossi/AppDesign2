using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainUtilities.CustomExceptions
{
    public class UserRoleIncorrectException : DomainValidationException
    {
        public UserRoleIncorrectException()
        {
            this.messageToDisplay = "User's role must be tester, developer or admin";
        }
    }
}
