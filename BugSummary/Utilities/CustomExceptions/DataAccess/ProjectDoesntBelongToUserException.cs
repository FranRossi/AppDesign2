using System;
using Utilities.CustomExceptions;

namespace CustomExceptions
{
    public class ProjectDoesntBelongToUserException : DataAccessException
    {
        public ProjectDoesntBelongToUserException()
        {
            this.messageToDisplay = "The user is not assigned to the Project the bug belongs to.";
        }
    }
}