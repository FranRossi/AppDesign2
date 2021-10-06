using System;

namespace Utilities.CustomExceptions
{
    public class InexistentProjectException : DataAccessException
    {
        public InexistentProjectException()
        {
            this.messageToDisplay = "The entered bug does not exist.";
        }
    }
}