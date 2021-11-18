namespace Utilities.CustomExceptions.WebApi
{
    public class UserModelMissingFieldException : ModelMissingFieldsException
    {
        public UserModelMissingFieldException()
        {
            this.MessageToDisplay = "Missing Fields: Required -> Id, FirstName, LastName, UserName, Password, Email, Role.";

        }
    }
}