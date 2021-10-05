using System;

namespace DataAccess.Exceptions
{
    public class UserMustBeTesterException : Exception
    {
        public override string Message => "User's role must be tester for this action";
    }
}