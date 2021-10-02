using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainUtilities.CustomExceptions
{
    public class EmailIsIncorrectException : Exception
    {
        public override string Message => "Email has a wrong format";
    }
}
