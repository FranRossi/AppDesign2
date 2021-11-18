

namespace Utilities.CustomExceptions.DataAccess
{
    public class UsernameIsNotUniqueException : DataAccessException
    {
        public UsernameIsNotUniqueException()
        {
            this.MessageToDisplay = "The username chosen was already taken, please enter a different one.";
        }
    }
}