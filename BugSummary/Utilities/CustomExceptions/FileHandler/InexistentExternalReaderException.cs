using System;

namespace Utilities.CustomExceptions.FileHandler
{
    public class InexistentExternalReaderException : Exception
    {
        public override string Message => "The entered external reader file name does not exist, contact the admin to add it.";
    }
}