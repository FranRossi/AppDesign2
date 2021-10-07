using System;
using Utilities.CustomExceptions;

namespace CustomExceptions
{
    public class ProjectModelMissingFieldException : ModelMissingFieldsException
    {
        public ProjectModelMissingFieldException()
        {
            this.messageToDisplay = "Missing Fields: Required -> Name.";
        }
    }
}