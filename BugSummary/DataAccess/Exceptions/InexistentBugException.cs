using System;

namespace DataAccess.Exceptions
{
    public class InexistentBugException : Exception
    {
        public override string Message => "The entered bug does not exist.";
    }
}