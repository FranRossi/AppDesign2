using System;

namespace Utilities.CustomExceptions
{
    public class LoginException : Exception
    {
        public override string Message => "You have entered an invalid username or password";
    }
}