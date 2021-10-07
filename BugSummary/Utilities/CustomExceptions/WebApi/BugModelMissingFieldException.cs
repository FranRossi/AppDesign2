using System;
using Utilities.CustomExceptions;

namespace CustomExceptions
{
    public class BugModelMissingFieldException : DataAccessException
    {
        public BugModelMissingFieldException()
        {
            this.messageToDisplay = "Missing Fields: Required -> Id, Name, Description, Version, State, ProjectId.";
        }
    }
}