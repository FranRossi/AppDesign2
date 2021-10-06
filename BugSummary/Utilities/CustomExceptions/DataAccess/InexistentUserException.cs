using System;

namespace Utilities.CustomExceptions
{
    public class InexistentUserException : DataAccessException
    {
        public InexistentUserException()
        {
            this.messageToDisplay = "The entered bug does not exist.";
        }
    }
}