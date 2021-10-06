using System;

namespace Utilities.CustomExceptions
{
    public class ProjectNameIsNotUniqueException : DataAccessException
    {
        public ProjectNameIsNotUniqueException()
        {
            this.messageToDisplay = "The project name chosen was already taken, please enter a different name.";
        }
    }
}