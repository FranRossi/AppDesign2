using System;

namespace Utilities.CustomExceptions
{
    public class UsernameIsNotUniqueException : DataAccessException
    {
        public UsernameIsNotUniqueException()
        {
            this.messageToDisplay = "The username chosen was already taken, please enter a different one.";
        }
    }
}