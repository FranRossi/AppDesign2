namespace Domain.DomainUtilities.CustomExceptions
{
    public class UserPropertyIsNullException : DomainValidationException
    {
        public UserPropertyIsNullException()
        {
            messageToDisplay = "User has a value that it is null";
        }
    }
}