namespace Domain.DomainUtilities.CustomExceptions
{
    public class BugVersionLengthIncorrectException : DomainValidationException
    {
        public BugVersionLengthIncorrectException()
        {
            messageToDisplay = "Bug's version must be under 10 characters";
        }
    }
}