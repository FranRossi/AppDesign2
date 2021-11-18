using System;
namespace Domain.DomainUtilities.CustomExceptions
{
    public class InvalidBugFixingTimeException : DomainValidationException
    {
        public InvalidBugFixingTimeException()
        {
            this.messageToDisplay = "Bug fixing time should not be negative.";
        }
    }
}
