using System;
using Utilities.CustomExceptions;

namespace CustomExceptions
{
    public class UserModelMissingFieldException : ModelMissingFieldsException
    {
        public UserModelMissingFieldException()
        {
            this.messageToDisplay = "Missing Fields: Required -> FirstName, LastName, UserName, Password, Email, Role.";
        }
    }
}