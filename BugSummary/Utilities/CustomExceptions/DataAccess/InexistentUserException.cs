namespace Utilities.CustomExceptions.DataAccess
{
    public class InexistentUserException : DataAccessException
    {
        public InexistentUserException()
        {
            this.MessageToDisplay = "The entered user does not exist.";
        }
    }
}