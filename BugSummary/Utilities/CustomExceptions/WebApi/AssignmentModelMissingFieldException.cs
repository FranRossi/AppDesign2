using System;
using Utilities.CustomExceptions;

namespace CustomExceptions
{
    public class AssignmentModelMissingFieldException : ModelMissingFieldsException
    {
        public AssignmentModelMissingFieldException()
        {
            this.messageToDisplay = "Missing Fields: Required -> Id, Name, Duration, HourlyRate, ProjectId.";
        }
    }
}