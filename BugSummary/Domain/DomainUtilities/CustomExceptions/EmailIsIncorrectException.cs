namespace Domain.DomainUtilities.CustomExceptions
{
    public class EmailIsIncorrectException : DomainValidationException
    {
        public EmailIsIncorrectException()
        {
            messageToDisplay = "Email has a wrong format";
        }
    }
}