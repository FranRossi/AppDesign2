using System;
namespace Domain.DomainUtilities.CustomExceptions
{
    public class BugDescriptionLengthIncorrectException : DomainValidationException
    {
        public BugDescriptionLengthIncorrectException()
        {
            this.messageToDisplay = "Bug's description must be under 150 characters";
        }
    }
}
