namespace Utilities.CustomExceptions.DataAccess
{
    public class ProjectNameIsNotUniqueException : DataAccessException
    {
        public ProjectNameIsNotUniqueException()
        {
            this.MessageToDisplay = "The project name chosen was already taken, please enter a different name.";
        }
    }
}