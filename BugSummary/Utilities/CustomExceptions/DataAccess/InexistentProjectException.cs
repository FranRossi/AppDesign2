namespace Utilities.CustomExceptions.DataAccess
{
    public class InexistentProjectException : DataAccessException
    {
        public InexistentProjectException()
        {
            this.MessageToDisplay = "The entered project does not exist.";
        }
    }
}