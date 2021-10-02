using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainUtilities.CustomExceptions
{
    public class UserRoleIncorrectException : Exception
    {
        public override string Message => "User's rolemust be tester, developer or admin";
    }
}
