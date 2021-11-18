namespace Utilities.CustomExceptions.DataAccess
{
    public class InexistentBugException : DataAccessException
    {
        public InexistentBugException()
        {
            this.MessageToDisplay = "The entered bug does not exist.";
        }
    }
}