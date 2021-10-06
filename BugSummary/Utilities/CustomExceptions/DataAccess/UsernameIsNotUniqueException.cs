using System;

namespace Utilities.CustomExceptions
{
    public class UsernameIsNotUniqueException : Exception
    {
        public override string Message => "The username chosen was already taken, please enter a different one.";
    }
}