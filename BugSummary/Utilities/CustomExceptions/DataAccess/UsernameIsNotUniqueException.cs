using System;

namespace Utilities.CustomExceptions
{
    public class UsernameIsNotUniqueException : DataAccessException
    {
        public UsernameIsNotUniqueException()
        {
            this.messageToDisplay = "The entered bug does not exist.";
        }
    }
}