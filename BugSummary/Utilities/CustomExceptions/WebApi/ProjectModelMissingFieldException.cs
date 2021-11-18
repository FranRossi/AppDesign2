namespace Utilities.CustomExceptions.WebApi
{
    public class ProjectModelMissingFieldException : ModelMissingFieldsException
    {
        public ProjectModelMissingFieldException()
        {
            this.MessageToDisplay = "Missing Fields: Required -> Name.";
        }
    }
}