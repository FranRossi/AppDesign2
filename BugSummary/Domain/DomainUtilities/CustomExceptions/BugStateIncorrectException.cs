namespace Domain.DomainUtilities.CustomExceptions
{
    public class BugStateIncorrectException : DomainValidationException
    {
        public BugStateIncorrectException()
        {
            messageToDisplay = "Bug's state must be active (1) or fixed (0)";
        }
    }
}