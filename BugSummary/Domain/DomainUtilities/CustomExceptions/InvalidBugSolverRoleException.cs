using System;
namespace Domain.DomainUtilities.CustomExceptions
{
    public class InvalidBugSolverRoleException : DomainValidationException
    {
        public InvalidBugSolverRoleException()
        {
            this.messageToDisplay = "Bug fixers may only be Developers.";
        }
    }
}
