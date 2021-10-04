using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainUtilities.CustomExceptions
{
    public class InvalidProjectAssigneeRole : Exception
    {
        public override string Message => "Project asingnees must either be Developers or Testers.";
    }
}
