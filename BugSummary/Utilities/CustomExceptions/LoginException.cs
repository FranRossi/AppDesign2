using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.CustomExceptions
{
    public class LoginException : Exception
    {
        public override string Message => "You have entered an invalid username or password";
    }
}
