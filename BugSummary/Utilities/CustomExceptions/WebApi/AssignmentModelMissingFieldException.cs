
namespace Utilities.CustomExceptions.WebApi
{
    public class AssignmentModelMissingFieldException : ModelMissingFieldsException
    {
        public AssignmentModelMissingFieldException()
        {
            this.MessageToDisplay = "Missing Fields: Required -> Id, Name, Duration, HourlyRate, ProjectId.";
        }
    }
}