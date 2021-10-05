using System;

namespace Utilities.CustomExceptions
{
    public class InexistentProjectException : Exception
    {
        public override string Message => "The entered project does not exist.";
    }
}