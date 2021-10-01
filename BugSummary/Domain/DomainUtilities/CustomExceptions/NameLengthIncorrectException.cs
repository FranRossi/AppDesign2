using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainUtilities.CustomExceptions
{
    public class NameLengthIncorrectException : Exception
    {
        public override string Message => "Project's name must be under 30 characters";
    }
}
