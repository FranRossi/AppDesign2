namespace Domain.DomainUtilities.CustomExceptions
{
    public class AssignmentIdLengthIncorrectException : DomainValidationException
    {
        public AssignmentIdLengthIncorrectException()
        {
            messageToDisplay = "Assignment's id must be under 4 characters";
        }
    }
}