using System;

namespace DataAccess.Exceptions
{
    public class ProjectDoesntBelongToUserException : Exception
    {
        public override string Message => "The user is not assigned to the Project the bug belongs to";
    }
}