namespace Domain.DomainUtilities.CustomExceptions
{
    public class ProjectNameLengthIncorrectException : DomainValidationException
    {
        public ProjectNameLengthIncorrectException()
        {
            messageToDisplay = "Project's name must be under 30 characters";
        }
    }
}