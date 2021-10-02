using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Exceptions
{
    public class UserCannotCreateBugException : Exception
    {
        public override string Message => "Only tester can create bugs";
    }
}
