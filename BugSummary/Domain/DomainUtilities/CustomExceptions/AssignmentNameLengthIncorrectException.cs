namespace Domain.DomainUtilities.CustomExceptions
{
    public class AssignmentNameLengthIncorrectException : DomainValidationException
    {
        public AssignmentNameLengthIncorrectException()
        {
            messageToDisplay = "Assignment's name must be under 60 characters";
        }
    }
}