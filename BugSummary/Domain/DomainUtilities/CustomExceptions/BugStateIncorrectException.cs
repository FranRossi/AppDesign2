using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainUtilities.CustomExceptions
{
    public class BugStateIncorrectException : Exception
    {
        public override string Message => "Bug's state must be active or inactive";
    }
}
