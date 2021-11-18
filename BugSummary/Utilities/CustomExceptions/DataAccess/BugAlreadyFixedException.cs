namespace Utilities.CustomExceptions.DataAccess
{
    public class BugAlreadyFixedException : DataAccessException
    {
        public BugAlreadyFixedException()
        {
            this.MessageToDisplay = "The bug you are trying to fix is already fixed.";
        }
    }
}