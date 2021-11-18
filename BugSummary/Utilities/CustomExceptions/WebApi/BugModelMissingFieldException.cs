namespace Utilities.CustomExceptions.WebApi
{
    public class BugModelMissingFieldException : ModelMissingFieldsException
    {
        public BugModelMissingFieldException()
        {
            this.MessageToDisplay = "Missing Fields: Required -> Id, Name, Description, Version, State, ProjectId.";
        }
    }
}