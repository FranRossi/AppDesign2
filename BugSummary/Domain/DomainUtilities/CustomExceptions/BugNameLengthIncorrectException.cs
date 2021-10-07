namespace Domain.DomainUtilities.CustomExceptions
{
    public class BugNameLengthIncorrectException : DomainValidationException
    {
        public BugNameLengthIncorrectException()
        {
            messageToDisplay = "Bug's name must be under 60 characters";
        }
    }
}