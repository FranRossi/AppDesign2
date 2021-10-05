using System;

namespace Utilities.CustomExceptions
{
    public class InexistentUserException : Exception
    {
        public override string Message => "The entered user does not exist.";
    }
}