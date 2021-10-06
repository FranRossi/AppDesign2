using System;

namespace Domain.DomainUtilities.CustomExceptions
{
    public class InvalidProjectAssigneeRoleException : DomainValidationException
    {
        public override string Message => "Project asingnees must either be Developers or Testers.";
    }
}