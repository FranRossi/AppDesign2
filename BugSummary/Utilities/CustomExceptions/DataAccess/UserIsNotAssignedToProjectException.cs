

namespace Utilities.CustomExceptions.DataAccess
{
    public class UserIsNotAssignedToProjectException : DataAccessException
    {
        public UserIsNotAssignedToProjectException()
        {
            this.MessageToDisplay = "The user is not assigned to the Project the bug belongs to.";
        }
    }
}