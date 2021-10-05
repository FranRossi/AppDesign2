namespace Domain.DomainUtilities.CustomExceptions
{
    public class BugIdLengthIncorrectException : DomainValidationException
    {
        public BugIdLengthIncorrectException()
        {
            messageToDisplay = "Bug's id must be under 4 characters";
        }
    }
}