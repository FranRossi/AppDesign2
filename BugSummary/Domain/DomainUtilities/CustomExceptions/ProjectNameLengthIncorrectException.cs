using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainUtilities.CustomExceptions
{
    public class ProjectNameLengthIncorrectException : DomainValidationException
    {
        public ProjectNameLengthIncorrectException()
        {
            this.messageToDisplay = "Project's name must be under 30 characters";
        }
    }
}
