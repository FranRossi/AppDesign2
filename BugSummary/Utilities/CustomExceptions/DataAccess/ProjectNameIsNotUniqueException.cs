using System;

namespace Utilities.CustomExceptions
{
    public class ProjectNameIsNotUniqueException : Exception
    {
        public override string Message => "The project name chosen was already taken, please enter a different name";
    }
}