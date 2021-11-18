namespace Domain.DomainUtilities.CustomExceptions
{
    public class InvalidUserHourlyRateException : DomainValidationException
    {
        public InvalidUserHourlyRateException()
        {
            messageToDisplay = "User's hourly rate must be greater than 0.";
        }
    }
}