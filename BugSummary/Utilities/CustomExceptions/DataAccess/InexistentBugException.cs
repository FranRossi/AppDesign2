using System;
using Utilities.CustomExceptions;

namespace CustomExceptions
{
    public class InexistentBugException : DataAccessException
    {
        public InexistentBugException()
        {
            this.messageToDisplay = "The entered bug does not exist.";
        }
    }
}