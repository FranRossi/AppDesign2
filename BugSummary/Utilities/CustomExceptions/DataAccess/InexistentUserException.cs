using System;

namespace Utilities.CustomExceptions
{
    public class InexistentUserException : DataAccessException
    {
        public InexistentUserException()
        {
            this.messageToDisplay = "The entered user does not exist.";
        }
    }
}