using System;
using Utilities.CustomExceptions;

namespace CustomExceptions
{
    public class ProjectDoesntBelongToUserException : DataAccessException
    {
        public ProjectDoesntBelongToUserException()
        {
            this.messageToDisplay = "The entered bug does not exist.";
        }
    }
}