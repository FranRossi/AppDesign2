using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainUtilities.CustomExceptions
{
    public class BugDescriptionLengthIncorrectException : Exception
    {
        public override string Message => "Bug's description must be under 150 characters";
    }
}
