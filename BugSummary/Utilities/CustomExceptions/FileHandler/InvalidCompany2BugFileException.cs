using System;

namespace Utilities.CustomExceptions
{
    public class InvalidCompany2BugFileException : Exception
    {
        public override string Message => "Unexpected end of file has occurred. Please check file structure and retry.";
    }
}