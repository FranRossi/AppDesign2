using System;

namespace Domain.DomainUtilities.CustomExceptions
{
    public class InvalidProjectAssigneeRoleException : Exception
    {
        public override string Message => "Project asingnees must either be Developers or Testers.";
    }
}