namespace Domain.DomainUtilities.CustomExceptions
{
    public class UserRoleIncorrectException : DomainValidationException
    {
        public UserRoleIncorrectException()
        {
            messageToDisplay = "User's role must be tester, developer or admin";
        }
    }
}