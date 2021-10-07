using System;
using Utilities.CustomExceptions;

namespace CustomExceptions
{
    public class UserIsNotAssignedToProjectException : DataAccessException
    {
        public UserIsNotAssignedToProjectException()
        {
            this.messageToDisplay = "The user is not assigned to the Project the bug belongs to.";
        }
    }
}